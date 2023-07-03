using System;
using ME.ECS;
using UnityEngine.SceneManagement;
using WebSnake.Markers;
using WebSnake.Modules;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class RestartSystem : ISystem, IUpdate
    {
        private RPCId _restartRpc;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            var net = world.GetModule<NetworkModule>();
            net.RegisterObject(this);
            _restartRpc = net.RegisterRPC(new Action<Restart>(Restart_RPC).Method);
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        public void Update(in float deltaTime)
        {
            if (world.GetMarker(out Restart restart))
            {
                var net = world.GetModule<NetworkModule>();
                net.RPC(this, _restartRpc, restart);
            }
        }

        private void Restart_RPC(Restart _)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}