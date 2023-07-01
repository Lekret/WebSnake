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
        public GameObject TilePrefab;
        public GameObject SnakeView;
        
        public int GridWidth = 32;
        public int GridHeight = 32;
        public int CameraHeight = 30;
        public int SnakeLength = 3;
        public float SnakeMovementInterval = 0.33f;

        public ViewId SnakeViewId { get; private set; }
        
        protected override void OnConstruct()
        {
            SnakeViewId = world.RegisterViewSource(SnakeView);
        }

        protected override void OnDeconstruct()
        {
        }
    }
}