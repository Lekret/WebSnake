using ME.ECS;
using UnityEngine;
using WebSnake.Systems;

namespace WebSnake.Features.Input
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    [CreateAssetMenu(menuName = "Features/" + nameof(InputFeature), fileName = nameof(InputFeature))]
    public class InputFeature : Feature
    {
        public InputData InputData { get; set; }

        protected override void OnConstruct()
        {
            AddSystem<PlayerInputSystem>();
            AddSystem<PlayerInputCleanupSystem>();
        }

        protected override void OnDeconstruct()
        {
        }
    }
}