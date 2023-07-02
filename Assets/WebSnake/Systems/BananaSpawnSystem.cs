﻿using ME.ECS;
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
    public class BananaSpawnSystem : ISystem, IAdvanceTick
    {
        private Filter _appleCollectedFilter;
        private Filter _emptyGridTileFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _appleCollectedFilter = Filter.Create("AppleCollectedFilter-BananaSpawnSystem")
                .With<GameStatsChanged>()
                .Push();
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
            
            if (_appleCollectedFilter.Count <= 0)
                return;

            var configFeature = world.GetFeature<ConfigFeature>();
            var collectedApplesCount = world.ReadSharedData<ApplesCollected>().Value;
            if (collectedApplesCount % configFeature.ApplesCollectedToSpawnBanana != 0) 
                return;
            
            var tile = _emptyGridTileFilter.GetRandomEntity();
            if (tile.IsEmpty())
                return;
                    
            var banana = world.AddEntity("Banana")
                .Set<BananaTag>()
                .Set<CollectableTag>()
                .Set(new Nutrition {Value = configFeature.BananaNutrition})
                .Set(tile.Read<Position>());
            world.InstantiateView(configFeature.BananaViewId, banana);
            GridUtils.OccupyTile(tile, banana);
        }
    }
}