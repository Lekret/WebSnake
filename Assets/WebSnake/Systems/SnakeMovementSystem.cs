using System.Collections.Generic;
using ME.ECS;
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
                ref var movementIntervalAccum = ref snake.Get<MovementIntervalAccum>();
                movementIntervalAccum.Value += deltaTime;
                if (movementIntervalAccum.Value < snake.Read<MovementInterval>().Value)
                    continue;

                movementIntervalAccum.Value = 0f;
                ref var movementDirection = ref snake.Get<MovementDirection>();
                if (snake.Has<NewMovementDirection>())
                {
                    movementDirection.Value = snake.Read<NewMovementDirection>().Value;
                    snake.Remove<NewMovementDirection>();
                }

                GridUtils.DeoccupyTile(world, snake);
                ref var position = ref snake.Get<Position>();
                ref var previousPosition = ref snake.Get<PreviousPosition>();
                previousPosition.Value = position.Value;
                position.Value += movementDirection.Value;
                TryTeleport(ref position);

                var segments = PoolList<Entity>.Spawn(100);
                SnakeUtils.GetOrderedSnakeSegments(world, snake.id, segments);
                MoveSegments(segments);
                PoolList<Entity>.Recycle(segments);
                
                snake.SetOneShot<Moved>();
            }
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
                ref var prevPosition = ref curSegment.Get<PreviousPosition>();
                ref var curPosition = ref curSegment.Get<Position>();
                prevPosition.Value = curPosition.Value;
                curPosition.Value = prevSegment.Read<PreviousPosition>().Value;
                curSegment.Get<MovementDirection>().Value = curPosition.Value - prevPosition.Value;
                GridUtils.OccupyTile(world, curSegment);
            }
        }
    }
}