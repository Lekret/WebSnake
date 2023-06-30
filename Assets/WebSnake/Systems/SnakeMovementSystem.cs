using ME.ECS;
using UnityEngine;
using WebSnake.Components;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class SnakeMovementSystem : ISystem, IAdvanceTick
    {
        public Filter SnakeFilter;
        public Filter InputFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            SnakeFilter = Filter.Create("SnakeFilter-SnakeMovementSystem").With<SnakeTag>().Push();
            InputFilter = Filter.Create("InputFilter-SnakeMovementSystem").With<MovementDirectionInput>().Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            var directionInput = GetTotalMovementDirectionInput();
            var worldInputDirection = new Vector3(directionInput.x, 0, directionInput.y);
            
            foreach (var entity in SnakeFilter)
            {
                ref var currentDirection = ref entity.Get<MovementDirection>();
                if (worldInputDirection != Vector3.zero &&
                    Mathf.Approximately(Vector3.Dot(currentDirection.Value, worldInputDirection), 0))
                {
                    currentDirection.Value = worldInputDirection;
                }

                var speed = entity.Read<Speed>();
                ref var position = ref entity.Get<Position>();
                position.Value += currentDirection.Value * (speed.Value * deltaTime);
            }
        }

        private Vector2 GetTotalMovementDirectionInput()
        {
            var Result = Vector2.zero;
            
            foreach (var entity in InputFilter)
            {
                Result += entity.GetOneShot<MovementDirectionInput>().Value;
            }

            return Result;
        }
    }
}