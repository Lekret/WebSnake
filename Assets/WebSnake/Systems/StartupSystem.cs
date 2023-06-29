using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
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
    public sealed class StartupSystem : ISystem, IAdvanceTick
    {
        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            
        }

        void ISystemBase.OnDeconstruct()
        {
            
        }

        public void AdvanceTick(in float deltaTime)
        {
            if (world.HasSharedData<GameLaunched>())
                return;
            
            var webSocket = new WebSocketWrapper();
            webSocket.Connect("wss://dev.match.qubixinfinity.io/snake");
            world.SetSharedData(new GameWebSocket {Value = webSocket});
            world.AddEntity("CreateGameRequest", EntityFlag.DestroyWithoutComponents).Set(new SendRequest
            {
                Data = new CreateGameRequest(),
                ResponseType = typeof(CreateGameResponse)
            });
            world.SetSharedData(new GameLaunched());
        }
    }
}