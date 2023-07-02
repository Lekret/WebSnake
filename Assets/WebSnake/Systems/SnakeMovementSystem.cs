using System.Collections.Generic;
using ME.ECS;
using UnityEngine;
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

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _snakeFilter = Filter.Create("SnakeFilter-SnakeMovementSystem")
                .With<SnakeTag>()
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
                snake.Get<PreviousPosition>().Value = position.Value;
                position.Value += movementDirection.Value;
                GridUtils.OccupyTile(world, snake);
                snake.SetOneShot<Moved>();
            }
        }
    }
}