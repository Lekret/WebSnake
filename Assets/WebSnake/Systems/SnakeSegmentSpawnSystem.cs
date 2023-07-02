using System.Collections.Generic;
using ME.ECS;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Features.Config;
using WebSnake.Utils;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public class SnakeSegmentSpawnSystem : ISystem, IAdvanceTick
    {
        private Filter _snakeFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _snakeFilter = Filter.Create("SnakeFilter-SnakeSegmentSpawnSystem")
                .With<SnakeTag>()
                .OnChanged<BodyLength>()
                .Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            foreach (var snake in _snakeFilter)
            {
                var segments = PoolList<Entity>.Spawn(100);
                SpawnSegmentsForBodyLength(snake, segments);
                PoolList<Entity>.Recycle(segments);
            }
        }

        private void SpawnSegmentsForBodyLength(Entity snake, List<Entity> segments)
        {
            SnakeUtils.GetOrderedSnakeSegments(world, snake.id, segments);
            if (segments.Count <= 0)
            {
                Debug.LogError("You probably forgot to create snake head");
                return;
            }
            
            var configFeature = world.GetFeature<ConfigFeature>();
            var bodyLength = snake.Read<BodyLength>();
            for (var idx = segments.Count; idx < bodyLength.Value; idx++)
            {
                var prevSegment = segments[idx - 1];
                var prevMovementDirection = prevSegment.Read<MovementDirection>();
                var segmentPosition = prevSegment.Read<PreviousPosition>().Value;
                var segmentEntity = world.AddEntity("SnakeSegment")
                    .Set<SnakeSegmentTag>()
                    .Set(new SnakeSegmentIndex {Value = idx})
                    .Set(new ParentId {Value = snake.id})
                    .Set(new Position {Value = segmentPosition})
                    .Set(new Rotation {Value = Quaternion.identity})
                    .Set(new PreviousPosition {Value = segmentPosition - prevMovementDirection.Value})
                    .Set(prevMovementDirection);
                world.InstantiateView(configFeature.SnakeSegmentView, segmentEntity);
                GridUtils.OccupyTile(world, segmentEntity);
                prevSegment.Remove<SnakeTailTag>();
                segmentEntity.Set<SnakeTailTag>();
                segments.Add(segmentEntity);

                Debug.Log($"Created ({idx}) snake segment  at {segmentPosition}");
            }
        }
    }
}