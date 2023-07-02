using ME.ECS;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Features.Config;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class CollectableRotationSystem : ISystem, IAdvanceTick
    {
        private Filter _filter;
        
        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _filter = Filter.Create("CollectableRotationSystem-CollectableRotationSystem")
                .With<CollectableTag>()
                .With<Rotation>()
                .Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            var configFeature = world.GetFeature<ConfigFeature>();
            
            foreach (var entity in _filter)
            {
                ref var rotation = ref entity.Get<Rotation>();
                rotation.Value *= Quaternion.AngleAxis(configFeature.CollectablesRotationSpeed * deltaTime, Vector3.up);
            }
        }
    }
}