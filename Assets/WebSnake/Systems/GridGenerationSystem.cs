using ME.ECS;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Features.Config;
using WebSnake.Features.Gameplay;
using Grid = WebSnake.Components.Grid;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class GridGenerationSystem : ISystem, IAdvanceTick
    {
        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (!world.HasSharedDataOneShot<GenerateGrid>())
                return;

            var configFeature = world.GetFeature<ConfigFeature>();
            if (!configFeature)
            {
                Debug.LogError("Config feature is null");
                return;
            }

            var generateGrid = world.GetSharedDataOneShot<GenerateGrid>();

            world.AddEntity("Grid")
                .Set<Grid>()
                .Set(new GridSize
                {
                    Width = generateGrid.Width,
                    Height = generateGrid.Height
                });

            Transform tilesParent = null;
#if UNITY_EDITOR
            tilesParent = new GameObject("Tiles").transform;
#endif

            for (var x = 0; x < generateGrid.Width; x++)
            for (var z = 0; z < generateGrid.Height; z++)
            {
                var spawnPosition = new Vector3(x, 0, z);
                Object.Instantiate(
                    configFeature.TilePrefab,
                    spawnPosition,
                    Quaternion.identity,
                    tilesParent);
            }
        }
    }
}