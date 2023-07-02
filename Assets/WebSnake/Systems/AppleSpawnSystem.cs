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
    public class AppleSpawnSystem : ISystem, IAdvanceTick
    {
        private Filter _applesFilter;
        private Filter _emptyGridTiles;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _applesFilter = Filter.Create().With<AppleTag>().Push();
            _emptyGridTiles = Filter.Create()
                .With<GridTileTag>()
                .With<Position>()
                .Without<OccupiedById>()
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
            var applesToAdd = configFeature.MaxApplesOnLevel - _applesFilter.Count;
            if (applesToAdd <= 0)
                return;

            for (var i = 0; i < applesToAdd; i++)
            {
                var tile = _emptyGridTiles.GetRandomEntity();
                if (tile.IsEmpty())
                {
                    Debug.LogError("RandomTile is empty entity");
                    continue;
                }

                var apple = world.AddEntity("Apple")
                    .Set<AppleTag>()
                    .Set(new Nutrition {Value = configFeature.AppleNutrition})
                    .Set(tile.Read<Position>());
                world.InstantiateView(configFeature.AppleViewId, apple);
                GridUtils.OccupyTile(apple, tile);
            }
        }
    }
}