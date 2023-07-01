using ME.ECS;
using WebSnake.Components;
using WebSnake.Web;
using GameWebSocket = WebSnake.Web.GameWebSocket;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class CreateGameSystem : ISystem, IAdvanceTick
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
            if (world.HasSharedData<GameLaunchedTag>())
                return;

            var webSocket = new GameWebSocket();
            webSocket.Connect("wss://dev.match.qubixinfinity.io/snake");
            world.SetSharedData(new Components.GameWebSocket {Value = webSocket});
            world.AddEntity("CreateGameRequest", EntityFlag.DestroyWithoutComponents).Set(new SendRequest
            {
                Data = new CreateGameRequest(),
                ResponseType = typeof(CreateGameResponse)
            });
            world.SetSharedData(new GameLaunchedTag());
        }
    }
}