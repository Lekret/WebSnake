using ME.ECS;
using UnityEngine;
using WebSnake.Components;

namespace WebSnake.Features.SharedFilters
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    [CreateAssetMenu(menuName = "Features/" + nameof(SharedFiltersFeature), fileName = nameof(SharedFiltersFeature))]
    public class SharedFiltersFeature : Feature
    {
        public Filter SnakeSegmentFilter { get; private set; } = Filter.Empty;
        public Filter GridFilter { get; private set; } = Filter.Empty;
        
        protected override void OnConstruct()
        {
            SnakeSegmentFilter = Filter.Create("SnakeSegmentFilter-SharedFilterFeature")
                .With<SnakeSegmentTag>()
                .With<SnakeSegmentIndex>()
                .Push();
            
            GridFilter = Filter.Create("GridFilter-SharedFilterFeature")
                .With<GridTag>()
                .With<PositionToTile>()
                .Push();
        }

        protected override void OnDeconstruct()
        {
            SnakeSegmentFilter = Filter.Empty;
            GridFilter = Filter.Empty;
        }
    }
}