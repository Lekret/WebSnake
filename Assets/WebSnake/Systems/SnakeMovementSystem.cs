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
        private Filter _segmentFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _snakeFilter = Filter.Create("SnakeFilter-SnakeMovementSystem")
                .With<SnakeTag>()
                .Without<DeadTag>()
                .Push();

            _segmentFilter = Filter.Create("SegmentFilter-SnakeMovementSystem")
                .With<SnakeSegmentTag>()
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

                ref var position = ref snake.Get<Position>();
                snake.Get<PreviousPosition>().Value = position.Value;
                position.Value += new Vector3(movementDirection.Value.x, 0, movementDirection.Value.y);

                var segmentBuffer = PoolList<Entity>.Spawn(100);
                SnakeUtils.GetOrderedSegments(_segmentFilter, segmentBuffer, snake.id);
                MoveSegments(segmentBuffer);
                PoolList<Entity>.Recycle(segmentBuffer);
            }
        }

        private void MoveSegments(List<Entity> segmentsBuffer)
        {
            var onlyHeadExists = segmentsBuffer.Count <= 1;
            if (onlyHeadExists) 
                return;
            
            for (var i = 1; i < segmentsBuffer.Count; i++)
            {
                var prevSegment = segmentsBuffer[i - 1];
                var currentSegment = segmentsBuffer[i];
                ref var curPosition = ref currentSegment.Get<Position>();
                currentSegment.Get<PreviousPosition>().Value = curPosition.Value;
                curPosition.Value = prevSegment.Read<PreviousPosition>().Value;
            }
        }
    }
}