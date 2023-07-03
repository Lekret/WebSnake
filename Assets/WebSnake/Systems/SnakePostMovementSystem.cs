using ME.ECS;
using WebSnake.Components;
using WebSnake.Utils;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class SnakePostMovementSystem : ISystem, IAdvanceTick
    {
        private Filter _snakeFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _snakeFilter = Filter.Create("SnakeFilter-PostSnakeMovementSystem")
                .With<SnakeTag>()
                .With<Moved>()
                .With<Position>()
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
                var position = snake.Read<Position>();
                var tile = GridUtils.GetTileAtPosition(world, position.Value);
                var result = SnakeUtils.HandleSnakeTileInteraction(world, snake, tile);
                if (result == TileInteractionResult.Unoccupied ||
                    result == TileInteractionResult.Collectable)
                {
                    GridUtils.OccupyTile(tile, snake);
                }
            }
        }
    }
}