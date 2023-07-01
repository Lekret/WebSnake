using System;
using ME.ECS;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Markers;
using WebSnake.Modules;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class SnakeHandleInputSystem : ISystem, IUpdate
    {
        private Filter _snakeFilter;
        private RPCId _inputMovementDirectionRpc;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _snakeFilter = Filter.Create("SnakeFilter-SnakeMovementSystem").With<SnakeTag>().Without<Dead>().Push();
            var net = world.GetModule<NetworkModule>();
            net.RegisterObject(this);
            _inputMovementDirectionRpc = net
                .RegisterRPC(new Action<InputMovementDirection>(InputMovementDirection_RPC).Method);
        }

        void ISystemBase.OnDeconstruct()
        {
        }
        
        public void Update(in float deltaTime)
        {
            if (world.GetMarker(out InputMovementDirection input))
            {
                var net = world.GetModule<NetworkModule>();
                net.RPC(this, _inputMovementDirectionRpc, input);
            }
        }

        private void InputMovementDirection_RPC(InputMovementDirection input)
        {
            foreach (var entity in _snakeFilter)
            {
                var movementDirection = entity.Read<MovementDirection>();
                if (Mathf.Approximately(Vector2.Dot(movementDirection.Value, input.Value), 0))
                {
                    entity.Set(new NewMovementDirection
                    {
                        Value = input.Value
                    });
                }
            }
        }
    }
}