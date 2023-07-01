using ME.ECS;
using WebSnake.Components;
using WebSnake.Features.Config;
using WebSnake.Web;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class GameCreatedSystem : ISystem, IAdvanceTick
    {
        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (!world.HasSharedData<GameWebSocket>())
                return;

            var webSocket = world.ReadSharedData<GameWebSocket>();
            while (webSocket.Value.TryRead(out CreateGameResponse createGameResponse))
            {
                StartGame(createGameResponse);
            }
        }

        private void StartGame(CreateGameResponse createGameResponse)
        {
            var configFeature = world.GetFeature<ConfigFeature>();
            world.SetSharedData(new GameId {Value = createGameResponse.Payload.Id});
            world.SetSharedData(new CollectedApplesCount {Value = createGameResponse.Payload.CollectedApples});
            world.SetSharedDataOneShot(new GenerateGrid
            {
                Width = configFeature ? configFeature.GridWidth : 32,
                Height = configFeature ? configFeature.GridHeight : 32
            });
            world.SetSharedDataOneShot(new SpawnSnake());
            world.SetSharedData(new GameLoaded());
        }
    }
}