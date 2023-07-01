using WebSnake.Modules.FakeNetworking;

namespace WebSnake.Modules {
    
    using TState = WebSnakeState;
    
    /// <summary>
    /// We need to implement our own NetworkModule class without any logic just to catch your State type into ECS.Network
    /// You can use some overrides to setup history config for your project
    /// </summary>
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class NetworkModule : ME.ECS.Network.NetworkModule<TState>
    {
        protected override int GetRPCOrder() 
        {
            return this.world.id;
        }

        protected override ME.ECS.Network.NetworkType GetNetworkType() 
        {
            return ME.ECS.Network.NetworkType.SendToNet | ME.ECS.Network.NetworkType.RunLocal;
        }

        protected override void OnInitialize() 
        {
            var instance = (ME.ECS.Network.INetworkModuleBase)this;
            instance.SetTransporter(new FakeTransporter(GetNetworkType()));
            instance.SetSerializer(new FSSerializer());
        }
    }
}