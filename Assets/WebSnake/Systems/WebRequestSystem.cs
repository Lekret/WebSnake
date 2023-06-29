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
    public class WebRequestSystem : ISystem, IAdvanceTick
    {
        public Filter Filter;
        
        public World world { get; set; }
        
        public void OnConstruct()
        {
            Filter.Create("Filter-WebRequestSystem").With<SendRequest>().Push(ref Filter);
        }

        public void OnDeconstruct()
        {
            
        }

        public void AdvanceTick(in float deltaTime)
        {
            if (!world.HasSharedData<GameWebSocket>())
                return;

            var socket = world.ReadSharedData<GameWebSocket>();
            
            foreach (var entity in Filter)
            { 
                var sendRequest = entity.Read<SendRequest>();
                socket.Value.SendData(sendRequest.Data, sendRequest.ResponseType);
                entity.Remove<SendRequest>();
            }
        }
    }
}