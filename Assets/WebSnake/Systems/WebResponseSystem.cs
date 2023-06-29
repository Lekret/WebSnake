using ME.ECS;
using Newtonsoft.Json;
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
    public class WebResponseSystem : ISystem, IUpdate
    {
        public World world { get; set; }

        public void OnConstruct()
        {
        }

        public void OnDeconstruct()
        {
        }

        public void Update(in float deltaTime)
        {
            if (!world.HasSharedData<GameWebSocket>())
                return;

            var webSocket = world.ReadSharedData<GameWebSocket>();
            while (webSocket.Value.TryRead(out CreateGameResponse createGameResponse))
            {
#if UNITY_EDITOR
                Debug.Log($"CreateGameResponse: {JsonConvert.SerializeObject(createGameResponse)}");
#endif
            }

            while (webSocket.Value.TryRead(out EndGameResponse endGameResponse))
            {
            }
        }
    }
}