using ME.ECS;
using UnityEngine;

namespace WebSnake.Features.Config
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    [CreateAssetMenu(menuName = "Features/" + nameof(ConfigFeature), fileName = nameof(ConfigFeature))]
    public class ConfigFeature : Feature
    {
        public int GridWidth = 32;
        public int GridHeight = 32;
        public int CameraHeight = 30;
        public int SnakeLength = 1;

        protected override void OnConstruct()
        {
            
        }

        protected override void OnDeconstruct()
        {
        }
    }
}