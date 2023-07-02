using ME.ECS;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Web;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class GameStatsChangedSystem : ISystem, IAdvanceTick
    {
        private Filter _gameStatsChangedFilter;
        private Filter _snakeFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _gameStatsChangedFilter = Filter.Create("GameStatsChangedFilter-AppleCollectedSystem")
                .With<GameStatsChanged>()
                .Push();

            _snakeFilter = Filter.Create("SnakeFilter-AppleCollectedSystem")
                .With<SnakeTag>()
                .With<BodyLength>()
                .Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (_gameStatsChangedFilter.Count == 0)
                return;

            world.AddEntity(nameof(GameStatsChangedRequest), EntityFlag.DestroyWithoutComponents)
                .Set(new SendRequest
                {
                    Data = new GameStatsChangedRequest(
                        applesCount: world.ReadSharedData<ApplesCollected>().Value,
                        snakeLength: GetSnakeLength(),
                        gameId: world.ReadSharedData<GameId>().Value),
                    ResponseType = null
                });
        }

        private int GetSnakeLength()
        {
            foreach (var snake in _snakeFilter)
                return snake.Read<BodyLength>().Value;

            Debug.LogError("Snake not found");
            return 0;
        }
    }
}