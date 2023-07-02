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
    public class AppleCollectedSystem : ISystem, IAdvanceTick
    {
        private Filter _appleCollectedFilter;
        private Filter _snakeFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _appleCollectedFilter = Filter.Create("AppleCollectedFilter-AppleCollectedSystem")
                .With<AppleCollected>()
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
            if (_appleCollectedFilter.Count == 0)
                return;

            ref var applesCollected = ref world.GetSharedData<ApplesCollected>();
            applesCollected.Value += _appleCollectedFilter.Count;

            world.AddEntity(nameof(AppleCollectedRequest), EntityFlag.DestroyWithoutComponents)
                .Set(new SendRequest
                {
                    Data = new AppleCollectedRequest(
                        applesCount: applesCollected.Value,
                        snakeLength: GetSnakeLength(),
                        gameId: world.ReadSharedData<GameId>().Value),
                    ResponseType = typeof(CreateGameResponse)
                });
        }

        private int GetSnakeLength()
        {
            foreach (var snake in _snakeFilter)
            {
                return snake.Read<BodyLength>().Value;
            }

            Debug.LogError("Snake not found");
            return 0;
        }
    }
}