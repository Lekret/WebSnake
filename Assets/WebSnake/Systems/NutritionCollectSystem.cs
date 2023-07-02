﻿using ME.ECS;
using WebSnake.Components;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public class NutritionCollectSystem : ISystem, IAdvanceTick
    {
        private Filter _filter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _filter = Filter
                .Create("CollectFilter-CollectSystem")
                .With<CollectedBy>()
                .With<Nutrition>()
                .Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            foreach (var collected in _filter)
            {
                var nutrition = collected.Read<Nutrition>();
                var collectedBy = collected.ReadOneShot<CollectedBy>();
                var collector = world.GetEntityById(collectedBy.Value);
                if (collector.Has<BodyLength>())
                {
                    collector.Get<BodyLength>().Value += nutrition.Value;
                }
                collected.Destroy();
            }
        }
    }
}