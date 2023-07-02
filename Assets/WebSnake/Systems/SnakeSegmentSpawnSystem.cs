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
                .With<Position>()
                .OnChanged<BodyLength>()
                .Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            var configFeature = world.GetFeature<ConfigFeature>();
            
            foreach (var snake in _snakeFilter)
            {
                var segmentBuffer = PoolList<Entity>.Spawn(100);
                SnakeUtils.GetOrderedSnakeSegments(world, snake.id, segmentBuffer);
                if (segmentBuffer.Count <= 0)
                {
                    Debug.LogError("You probably forgot to create snake head");
                    continue;
                }

                var bodyLength = snake.Read<BodyLength>();
                for (var idx = segmentBuffer.Count; idx < bodyLength.Value; idx++)
                {
                    var prevSegment = segmentBuffer[idx - 1];
                    var prevMovementDirection = prevSegment.Read<MovementDirection>();
                    var segmentPosition = prevSegment.Read<PreviousPosition>().Value;
                    var segmentEntity = world.AddEntity("SnakeSegment")
                        .Set<SnakeSegmentTag>()
                        .Set(new SnakeSegmentIndex {Value = idx})
                        .Set(new ParentId {Value = snake.id})
                        .Set(new Position {Value = segmentPosition})
                        .Set(new PreviousPosition {Value = segmentPosition - prevMovementDirection.Value})
                        .Set(prevMovementDirection);
                    world.InstantiateView(configFeature.SnakeSegmentView, segmentEntity);
                    GridUtils.OccupyTile(world, segmentEntity);
                    segmentBuffer.Add(segmentEntity);
                    
                    Debug.Log($"Created ({idx}) snake segment  at ({segmentPosition})");
                }

                PoolList<Entity>.Recycle(segmentBuffer);
            }
        }
    }
}