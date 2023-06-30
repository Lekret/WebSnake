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
    public sealed class GameEndedSystem : ISystem, IAdvanceTick
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
            while (webSocket.Value.TryRead(out EndGameResponse response))
            {
                EndGame(response);
            }
        }

        private void EndGame(EndGameResponse response)
        {
            
        }
    }
}