using System.Collections.Generic;
using ME.ECS;
using WebSnake.Components;
using WebSnake.Utils;
using Vector3 = UnityEngine.Vector3;

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

                var segmentBuffer = PoolList<Entity>.Spawn(100);
                SnakeUtils.GetOrderedSnakeSegments(world, snake.id, segmentBuffer);
                MoveSegments(segmentBuffer);
                PoolList<Entity>.Recycle(segmentBuffer);
                
                TryCollectAtTile(position.Value, snake.id);
                GridUtils.OccupyTile(world, snake);
            }
        }

        private void TryTeleport(ref Position snakePosition)
        {
            foreach (var grid in _gridFilter)
            {
                var gridSize = grid.Read<GridSize>();

                if (snakePosition.Value.z < 0)
                    snakePosition.Value.z = gridSize.Height - 1;
                else if (snakePosition.Value.z >= gridSize.Height)
                    snakePosition.Value.z = 0;
                else if (snakePosition.Value.x < 0)
                    snakePosition.Value.x = gridSize.Width - 1;
                else if (snakePosition.Value.x >= gridSize.Width)
                    snakePosition.Value.x = 0;
            }
        }

        private void MoveSegments(IReadOnlyList<Entity> segmentsBuffer)
        {
            var onlyHeadExists = segmentsBuffer.Count <= 1;
            if (onlyHeadExists) 
                return;
            
            for (var i = 1; i < segmentsBuffer.Count; i++)
            {
                var prevSegment = segmentsBuffer[i - 1];
                var curSegment = segmentsBuffer[i];
                GridUtils.DeoccupyTile(world, curSegment);
                ref var prevPosition = ref curSegment.Get<PreviousPosition>();
                ref var curPosition = ref curSegment.Get<Position>();
                prevPosition.Value = curPosition.Value;
                curPosition.Value = prevSegment.Read<PreviousPosition>().Value;
                curSegment.Get<MovementDirection>().Value = curPosition.Value - prevPosition.Value;
                GridUtils.OccupyTile(world, curSegment);
            }
        }
        
        private void TryCollectAtTile(Vector3 tilePosition, int snakeId)
        {
            var tile = GridUtils.GetTileAtPosition(world, tilePosition);
            if (!tile.Has<OccupiedBy>())
                return;
            
            var occupantId = tile.Read<OccupiedBy>().Value;
            var occupant = world.GetEntityById(occupantId);
            occupant.SetOneShot(new CollectedBy
            {
                Value = snakeId
            });
            GridUtils.DeoccupyTile(tile);
        }
    }
}