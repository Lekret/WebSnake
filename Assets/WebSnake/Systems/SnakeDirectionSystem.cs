using ME.ECS;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Features.Input;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class SnakeDirectionSystem : ISystem, IAdvanceTick
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
            var directionInput = ReadDirectionInput();
            if (directionInput == Vector2.zero)
                return;
            
            foreach (var entity in _snakeFilter)
            {
                var movementDirection = entity.Read<MovementDirection>();
                if (Mathf.Approximately(Vector2.Dot(movementDirection.Value, directionInput), 0))
                {
                    entity.Set(new NewMovementDirection
                    {
                        Value = directionInput
                    });
                }
            }
        }

        private Vector2Int ReadDirectionInput()
        {
            var inputFeature = world.GetFeature<InputFeature>();
            if (inputFeature)
                return inputFeature.InputData.MovementDirection;

            return Vector2Int.zero;
        }
    }
}