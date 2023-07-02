using ME.ECS;
using WebSnake.Components;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class LifetimeSystem : ISystem, IAdvanceTick
    {
        private Filter _filter;
        
        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _filter = Filter.Create("Filter-LifetimeSystem")
                .With<Lifetime>()
                .Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var lifetime = ref entity.Get<Lifetime>();
                lifetime.Value -= deltaTime;
                if (lifetime.Value <= 0)
                {
                    entity.Remove<Lifetime>();
                    entity.Set<LifetimeExpiredTag>();
                }
            }
        }
    }
}