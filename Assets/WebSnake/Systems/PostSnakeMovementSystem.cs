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
    public class PostSnakeMovementSystem : ISystem, IAdvanceTick
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
                if (tile.Has<OccupiedBy>())
                {
                    var occupantId = tile.Read<OccupiedBy>().Value;
                    var occupant = world.GetEntityById(occupantId);
                    if (occupant.Has<SnakeSegmentTag>())
                    {
                        snake.Set<DeadTag>();
                        continue;
                    }

                    if (occupant.Has<CollectableTag>())
                    {
                        occupant.SetOneShot(new CollectedBy {Value = snake.id});
                        GridUtils.DeoccupyTile(tile);
                    }
                }
                
                GridUtils.OccupyTile(tile, snake);
            }
        }
    }
}