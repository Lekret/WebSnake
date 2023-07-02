using System.Collections.Generic;
using ME.ECS;
using WebSnake.Components;
using WebSnake.Utils;

namespace WebSnake.Systems
{
    public class SnakeSegmentMoveSystem : ISystem, IAdvanceTick
    {
        private Filter _snakeFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _snakeFilter = Filter
                .Create("Filter-SnakeFilter")
                .With<SnakeTag>()
                .With<Moved>()
                .Without<DeadTag>()
                .Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            foreach (var snake in _snakeFilter)
            {
                var segmentBuffer = PoolList<Entity>.Spawn(100);
                SnakeUtils.GetOrderedSnakeSegments(world, snake.id, segmentBuffer);
                MoveSegments(segmentBuffer);
                PoolList<Entity>.Recycle(segmentBuffer);
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
    }
}