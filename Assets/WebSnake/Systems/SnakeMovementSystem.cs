using System.Collections.Generic;
using ME.ECS;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Utils;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class SnakeMovementSystem : ISystem, IAdvanceTick
    {
        private Filter _snakeFilter;
        private Filter _gridFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _snakeFilter = Filter.Create("SnakeFilter-SnakeMovementSystem")
                .With<SnakeTag>()
                .Without<DeadTag>()
                .Push();
            
            _gridFilter = Filter.Create("GridFilter-SnakeMovementSystem")
                .With<GridTag>()
                .With<GridSize>()
                .Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            foreach (var snake in _snakeFilter)
            {
                UpdateMovementInterval(snake, deltaTime, out var canMove);
                if (!canMove)
                    continue;
                
                Vector3? newMovementDirection = null;
                if (snake.Has<NewMovementDirection>())
                {
                    newMovementDirection = snake.Read<NewMovementDirection>().Value;
                    snake.Remove<NewMovementDirection>();
                }
                
                GridUtils.DeoccupyTile(world, snake);
                MoveSegment(snake, newMovementDirection);
                var segments = PoolList<Entity>.Spawn(100);
                SnakeUtils.GetOrderedSnakeSegments(world, snake.id, segments);
                MoveSegments(segments);
                PoolList<Entity>.Recycle(segments);
            }
        }

        private static void UpdateMovementInterval(Entity snake, float deltaTime, out bool canMove)
        {
            ref var movementIntervalAccum = ref snake.Get<MovementIntervalAccum>();
            movementIntervalAccum.Value += deltaTime;
            if (movementIntervalAccum.Value < snake.Read<MovementInterval>().Value)
            {
                canMove = false;
            }
            else
            {
                movementIntervalAccum.Value = 0f;
                canMove = true;
            }
        }

        private void MoveSegments(IReadOnlyList<Entity> segments)
        {
            var onlyHeadExists = segments.Count <= 1;
            if (onlyHeadExists) 
                return;
            
            for (var i = 1; i < segments.Count; i++)
            {
                var prevSegment = segments[i - 1];
                var curSegment = segments[i];
                GridUtils.DeoccupyTile(world, curSegment);
                MoveSegment(curSegment, prevSegment.Read<OldMovementDirection>().Value);
                GridUtils.OccupyTile(world, curSegment);
            }
        }

        private void MoveSegment(Entity segment, Vector3? newMovementDirection)
        {
            ref var position = ref segment.Get<Position>();
            ref var movementDir = ref segment.Get<MovementDirection>();
            ref var oldMovementDir = ref segment.Get<OldMovementDirection>();
            oldMovementDir.Value = movementDir.Value;
            if (newMovementDirection.HasValue)
                movementDir.Value = newMovementDirection.Value;
            position.Value += movementDir.Value;
            TryTeleport(ref position);
            segment.SetOneShot<Moved>();
        }
        
        private void TryTeleport(ref Position segmentPosition)
        {
            foreach (var grid in _gridFilter)
            {
                var gridSize = grid.Read<GridSize>();

                if (segmentPosition.Value.z < 0)
                    segmentPosition.Value.z = gridSize.Height - 1;
                else if (segmentPosition.Value.z >= gridSize.Height)
                    segmentPosition.Value.z = 0;
                else if (segmentPosition.Value.x < 0)
                    segmentPosition.Value.x = gridSize.Width - 1;
                else if (segmentPosition.Value.x >= gridSize.Width)
                    segmentPosition.Value.x = 0;
            }
        }
    }
}