using ME.ECS;
using UnityEngine;
using WebSnake.Features.Input;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class PlayerInputSystem : ISystem, IUpdate
    {
        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IUpdate.Update(in float deltaTime)
        {
            var inputFeature = world.GetFeature<InputFeature>();
            if (!inputFeature)
                return;

            var direction = Vector2.zero;
            if (Input.GetKeyDown(KeyCode.W))
                direction.y += 1;

            if (Input.GetKeyDown(KeyCode.S))
                direction.y -= 1;

            if (Input.GetKeyDown(KeyCode.A))
                direction.x -= 1;

            if (Input.GetKeyDown(KeyCode.D))
                direction.x += 1;

            if (direction == Vector2.zero)
                return;

            inputFeature.InputData = new InputData
            {
                MovementDirection = direction
            };
        }
    }
}