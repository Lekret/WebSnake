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
        public GameObject SnakeSegmentView;
        public GameObject AppleView;
        public GameObject BananaView;
        
        [Header("Grid")]
        public int GridWidth = 32;
        public int GridHeight = 32;
        
        [Header("Snake")]
        public int SnakeLength = 3;
        public float SnakeMovementInterval = 0.33f;
        
        [Header("Food")]
        public int AppleNutrition = 1;
        public int MaxApplesOnLevel = 5;
        public int BananaNutrition = 2;
        public int ApplesCollectedToSpawnBanana;
        public float BananaLifetime;

        public ViewId SnakeViewId { get; private set; }
        public ViewId SnakeSegmentViewId { get; private set; }
        public ViewId AppleViewId { get; private set; }
        public ViewId BananaViewId { get; private set; }

        protected override void OnConstruct()
        {
            SnakeViewId = world.RegisterViewSource(SnakeView);
            SnakeSegmentViewId = world.RegisterViewSource(SnakeSegmentView);
            AppleViewId = world.RegisterViewSource(AppleView);
            BananaViewId = world.RegisterViewSource(BananaView);
        }

        protected override void OnDeconstruct()
        {
        }
    }
}