using ME.ECS;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Features.Config;
using WebSnake.Utils;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class AppleSpawnSystem : ISystem, IAdvanceTick
    {
        private Filter _appleFilter;
        private Filter _emptyGridTileFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _appleFilter = Filter.Create("AppleFilter-AppleSpawnSystem").With<AppleTag>().Push();
            _emptyGridTileFilter = Filter.Create("EmptyGridTileFilter-AppleSpawnSystem")
                .With<GridTileTag>()
                .With<Position>()
                .Without<OccupiedBy>()
                .Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (!world.HasSharedData<GameLoadedTag>())
                return;

            var configFeature = world.GetFeature<ConfigFeature>();
            var applesToAdd = configFeature.MaxApplesOnLevel - _appleFilter.Count;
            if (applesToAdd <= 0)
                return;

            for (var i = 0; i < applesToAdd; i++)
            {
                var tile = _emptyGridTileFilter.GetRandomEntity();
                if (tile.IsEmpty())
                    continue;

                var apple = world.AddEntity("Apple")
                    .Set<AppleTag>()
                    .Set<CollectableTag>()
                    .Set(new Nutrition {Value = configFeature.AppleNutrition})
                    .Set(tile.Read<Position>())
                    .Set(new Rotation {Value = Quaternion.identity});
                world.InstantiateView(configFeature.AppleViewId, apple);
                GridUtils.OccupyTile(tile, apple);
            }
        }
    }
}