using ME.ECS;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Features;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class GridGenerationSystem : ISystem, IAdvanceTick
    {
        public Filter Filter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            Filter.Create("Filter-GridGenerationSystem").With<GenerateGrid>().Push(ref Filter);
        }

        void ISystemBase.OnDeconstruct()
        {
            
        }
        
        public void AdvanceTick(in float deltaTime)
        {
            if (!world.HasSharedDataOneShot<GenerateGrid>())
                return;
            
            var feature = world.GetFeature<GameplayFeature>();
            if (feature == null)
                return;

            var generateGrid =  world.GetSharedDataOneShot<GenerateGrid>();
            for (var x = 0; x < generateGrid.Width; x++)
            {
                for (var z = 0; z < generateGrid.Height; z++)
                {
                    var spawnPosition = new Vector3(x, 0, z);
                    Object.Instantiate(feature.CellPrefab, spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}