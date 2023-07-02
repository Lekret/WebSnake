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
        private Filter _appleEatenFilter;
        private Filter _emptyGridTiles;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _appleEatenFilter = Filter.Create().With<AppleTag>().With<Collected>().Push();
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
            
            if (_appleEatenFilter.Count <= 0)
                return;

            var configFeature = world.GetFeature<ConfigFeature>();
            var collectedApplesCount = world.ReadSharedData<CollectedApplesCount>().Value;
            if (collectedApplesCount % configFeature.ApplesCollectedToSpawnBanana != 0) 
                return;
            
            var tile = _emptyGridTiles.GetRandomEntity();
            if (tile.IsEmpty())
            {
                Debug.LogError("RandomTile is empty entity");
                return;
            }
                    
            var banana = world.AddEntity("Banana")
                .Set<AppleTag>()
                .Set(new Nutrition {Value = configFeature.BananaNutrition})
                .Set(tile.Read<Position>());
            world.InstantiateView(configFeature.BananaViewId, banana);
            GridUtils.OccupyTile(banana, tile);
        }
    }
}