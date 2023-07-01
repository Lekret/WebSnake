using ME.ECS;
using ME.ECS.Views.Providers;
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
    public sealed class SnakeSpawnSystem : ISystem, IAdvanceTick
    {
        private Filter _gridFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _gridFilter = Filter.Create("GridFilter-SnakeSpawnSystem")
                .With<Grid>()
                .With<GridSize>()
                .Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (!world.HasSharedDataOneShot<SpawnSnake>())
                return;

            var configFeature = world.GetFeature<ConfigFeature>();
            var snakeEntity = world.AddEntity("Snake")
                .Set<SnakeTag>()
                .Set(new BodyLength {Value = configFeature.SnakeLength})
                .Set(new Position { Value = GetSnakePosition() })
                .Set<Rotation>()
                .Set(new MovementDirection {Value = Vector2Int.up})
                .Set(new MovementInterval {Value = configFeature.SnakeMovementInterval});
            world.InstantiateView(configFeature.SnakeViewId, snakeEntity);
        }

        private Vector3 GetSnakePosition()
        {
            foreach (var gridEntity in _gridFilter)
            {
                var gridSize = gridEntity.Read<GridSize>();
                var gridCenter = new Vector3(gridSize.Width / 2.0f, 0, gridSize.Height / 2.0f);
                return gridCenter;
            }

            Debug.LogError("Grid is not generated, can't calculate snake position");
            return Vector3.zero;
        }
    }
}