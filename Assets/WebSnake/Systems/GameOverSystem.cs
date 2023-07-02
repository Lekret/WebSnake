using ME.ECS;
using WebSnake.Components;
using WebSnake.Web;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class GameOverSystem : ISystem, IAdvanceTick
    {
        private Filter _snakeFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _snakeFilter = Filter.Create("SnakeFilter-GameOverSystem")
                .With<SnakeTag>()
                .OnChanged<DeadTag>()
                .Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (_snakeFilter.Count == 0)
                return;

            world.SetSharedData(new GameOverTag());
            world.AddEntity(nameof(EndGameRequest), EntityFlag.DestroyWithoutComponents)
                .Set(new SendRequest
                {
                    Data = new EndGameRequest(
                        world.ReadSharedData<GameId>().Value),
                    ResponseType = typeof(EndGameResponse)
                });
        }
    }
}