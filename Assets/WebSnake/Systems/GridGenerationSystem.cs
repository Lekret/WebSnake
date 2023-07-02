using System.Collections.Generic;
using ME.ECS;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Features.Config;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class GridGenerationSystem : ISystem, IAdvanceTick
    {
        private Filter _gridFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _gridFilter = Filter.Create("GridFilter-GridGenerationSystem").With<GridTag>().Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (!world.HasSharedDataOneShot<GenerateGrid>())
                return;

            if (_gridFilter.Count > 0)
            {
                Debug.LogError("Attempt to generate second grid");
                return;
            }

            var configFeature = world.GetFeature<ConfigFeature>();
            var generateGrid = world.ReadSharedDataOneShot<GenerateGrid>();

            var positionToTile = new Dictionary<Vector3, int>();
            var grid = world.AddEntity("Grid")
                .Set<GridTag>()
                .Set(new GridSize
                {
                    Width = generateGrid.Width,
                    Height = generateGrid.Height
                });

            for (var x = 0; x < generateGrid.Width; x++)
            {
                for (var z = 0; z < generateGrid.Height; z++)
                {
                    var tilePosition = new Vector3(x, 0, z);
                    var tile = world.AddEntity("Tile")
                        .Set<GridTileTag>()
                        .Set(new Position {Value = tilePosition});
                    world.InstantiateView(configFeature.TileViewId, tile);
                    positionToTile.Add(tilePosition, tile.id);
                }
            }

            grid.Set(new PositionToTile {Value = positionToTile});
        }
    }
}