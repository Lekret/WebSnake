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
    public sealed class BananaSpawnSystem : ISystem, IAdvanceTick
    {
        private Filter _emptyGridTileFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _emptyGridTileFilter = Filter.Create("EmptyGridTileFilter-BananaSpawnSystem")
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

            ref var lastBananaSpawnAppleCount = ref world.GetSharedData<LastBananaSpawnAppleCount>();
            var collectedApplesCount = world.ReadSharedData<ApplesCollected>();
            var unhandledApplesCount = collectedApplesCount.Value - lastBananaSpawnAppleCount.Value;
            if (unhandledApplesCount <= 0)
                return;
            
            var configFeature = world.GetFeature<ConfigFeature>();
            var bananasToSpawn = unhandledApplesCount / configFeature.ApplesCollectedToSpawnBanana;
            if (bananasToSpawn <= 0)
                return;
            
            lastBananaSpawnAppleCount.Value = collectedApplesCount.Value;
            
            for (var i = 0; i < bananasToSpawn; i++)
            {
                var tile = _emptyGridTileFilter.GetRandomEntity();
                if (tile.IsEmpty())
                    continue;

                var banana = world.AddEntity("Banana")
                    .Set<BananaTag>()
                    .Set<CollectableTag>()
                    .Set(new Nutrition {Value = configFeature.BananaNutrition})
                    .Set(new Lifetime {Value = configFeature.BananaLifetime})
                    .Set(tile.Read<Position>())
                    .Set(new Rotation {Value = Quaternion.identity});
                world.InstantiateView(configFeature.BananaViewId, banana);
                GridUtils.OccupyTile(tile, banana);
            }
        }
    }
}