using ME.ECS;
using UnityEngine;

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
        }

        protected override void OnDeconstruct()
        {
        }
    }
}