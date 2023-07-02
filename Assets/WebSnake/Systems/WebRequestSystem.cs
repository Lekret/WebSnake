using ME.ECS;
using WebSnake.Components;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class WebRequestSystem : ISystem, IAdvanceTick
    {
        private Filter _filter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _filter = Filter.Create("Filter-WebRequestSystem").With<SendRequest>().Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (!world.HasSharedData<GameWebSocketHolder>())
                return;

            var socket = world.ReadSharedData<GameWebSocketHolder>();

            foreach (var entity in _filter)
            {
                var sendRequest = entity.Read<SendRequest>();
                socket.Value.SendData(sendRequest.Data, sendRequest.ResponseType);
                entity.Remove<SendRequest>();
            }
        }
    }
}