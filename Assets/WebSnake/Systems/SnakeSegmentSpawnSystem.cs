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
    public sealed class SnakeSegmentSpawnSystem : ISystem, IAdvanceTick
    {
        private Filter _snakeFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _snakeFilter = Filter.Create("SnakeFilter-SnakeSegmentSpawnSystem")
                .With<SnakeTag>()
                .With<BodyLength>()
                .With<BodyLengthDirtyTag>()
                .Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            foreach (var snake in _snakeFilter)
            {
                snake.Remove<BodyLengthDirtyTag>();
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
            var segmentsCreated = 0;
            for (var idx = segments.Count; idx < bodyLength.Value; idx++)
            {
                var prevSegment = segments[idx - 1];
                var prevMovementDirection = prevSegment.Read<MovementDirection>();
                var prevPosition = prevSegment.Read<Position>();
                var position = prevPosition.Value - prevMovementDirection.Value;
                var tile = GridUtils.GetTileAtPosition(world, position);
                if (tile.IsEmpty())
                {
                    GridUtils.TryTeleport(world, ref position);
                    tile = GridUtils.GetTileAtPosition(world, position);
                    var result = SnakeUtils.HandleSnakeTileInteraction(world, snake, tile);
                    if (result == TileInteractionResult.Dead)
                        return;
                }
                
                var segmentEntity = world.AddEntity("SnakeSegment")
                    .Set<SnakeSegmentTag>()
                    .Set(new SnakeSegmentIndex {Value = idx})
                    .Set(new ParentId {Value = snake.id})
                    .Set(new Position {Value = position})
                    .Set(new Rotation {Value = Quaternion.identity})
                    .Set(prevMovementDirection);
                world.InstantiateView(configFeature.SnakeSegmentViewId, segmentEntity);
                GridUtils.OccupyTile(world, segmentEntity);
                prevSegment.Remove<SnakeTailTag>();
                segmentEntity.Set<SnakeTailTag>();
                segments.Add(segmentEntity);

                segmentsCreated++;
                Debug.Log($"Created ({idx}) snake segment  at {position}");
            }

            if (segments.Count == configFeature.SnakeLength)
                return;

            ref var movementInterval = ref snake.Get<MovementInterval>();
            movementInterval.Value = Mathf.Max(
                movementInterval.Value - configFeature.MovementIntervalDecreaseForEachSegment * segmentsCreated,
                configFeature.MinSnakeMovementInterval);
        }
    }
}