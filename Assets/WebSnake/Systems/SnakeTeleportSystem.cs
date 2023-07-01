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
    public class SnakeTeleportSystem : ISystem, IAdvanceTick
    {
        private Filter _snakeFilter;
        private Filter _gridFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _snakeFilter = Filter.Create().With<SnakeTag>().With<Position>().Push();
            _gridFilter = Filter.Create().With<GridTag>().With<GridSize>().Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            foreach (var grid in _gridFilter)
            {
                var gridSize = grid.Read<GridSize>();

                foreach (var snake in _snakeFilter)
                {
                    ref var snakePosition = ref snake.Get<Position>();
                    var positionBeforeTeleport = snakePosition.Value;
                    if (snakePosition.Value.z < 0)
                        snakePosition.Value.z = gridSize.Height - 1;
                    else if (snakePosition.Value.z >= gridSize.Height)
                        snakePosition.Value.z = 0;
                    else if (snakePosition.Value.x < 0)
                        snakePosition.Value.x = gridSize.Width - 1;
                    else if (snakePosition.Value.x >= gridSize.Width)
                        snakePosition.Value.x = 0;

                    if (snakePosition.Value != positionBeforeTeleport)
                    {
                        snake.Get<PreviousPosition>().Value = positionBeforeTeleport;
                    }
                }
            }
        }
    }
}