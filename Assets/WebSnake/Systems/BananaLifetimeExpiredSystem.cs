using ME.ECS;
using WebSnake.Components;
using WebSnake.Utils;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class BananaLifetimeExpiredSystem : ISystem, IAdvanceTick
    {
        private Filter _filter;
        
        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _filter = Filter.Create("Filter-BananaLifetimeExpiredSystem")
                .With<BananaTag>()
                .With<LifetimeExpiredTag>()
                .Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            foreach (var entity in _filter)
            {
                GridUtils.DeoccupyTile(world, entity);
                entity.Destroy();
            }
        }
    }
}