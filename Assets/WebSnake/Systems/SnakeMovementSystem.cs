﻿using ME.ECS;
using UnityEngine;
using WebSnake.Components;
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
            _snakeFilter = Filter.Create("SnakeFilter-SnakeMovementSystem").With<SnakeTag>().Without<Dead>().Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            foreach (var entity in _snakeFilter)
            {
                ref var movementIntervalAccum = ref entity.Get<MovementIntervalAccum>();
                movementIntervalAccum.Value += deltaTime;
                if (movementIntervalAccum.Value < entity.Read<MovementInterval>().Value)
                    continue;

                movementIntervalAccum.Value = 0f;
                ref var movementDirection = ref entity.Get<MovementDirection>();
                if (entity.Has<NewMovementDirection>())
                {
                    movementDirection.Value = entity.Read<NewMovementDirection>().Value;
                    entity.Remove<NewMovementDirection>();
                }

                ref var position = ref entity.Get<Position>();
                position.Value += new Vector3(movementDirection.Value.x, 0, movementDirection.Value.y);
            }
        }
    }
}