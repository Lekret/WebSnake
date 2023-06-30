using ME.ECS;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Features;
using WebSnake.Markers;

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
            _snakeFilter = Filter.Create("SnakeFilter-SnakeMovementSystem").With<SnakeTag>().Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            var directionInput = ReadDirectionInput();
            var worldInputDirection = new Vector3(directionInput.x, 0, directionInput.y);

            foreach (var entity in _snakeFilter)
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

        private Vector2 ReadDirectionInput()
        {
            var gameplayFeature = world.GetFeature<GameplayFeature>();
            if (gameplayFeature)
                return gameplayFeature.SnakeInput.MovementDirection;
            
            return Vector2.zero;
        }
    }
}