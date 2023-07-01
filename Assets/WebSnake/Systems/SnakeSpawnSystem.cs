using System;
using System.Collections.Generic;
using ME.ECS;
using ME.ECS.Collections.LowLevel.Unsafe;
using ME.ECS.Views;
using Unity.Collections;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Features.Config;
using WebSnake.Features.Gameplay;
using WebSnake.Views;

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
                .With<GridTag>()
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

            var snake = CreateSnake();
            CreateSnakeCamera(snake.id);
        }

        private Entity CreateSnake()
        {
            var configFeature = world.GetFeature<ConfigFeature>();
            var snake = world.AddEntity("Snake")
                .Set<SnakeTag>()
                .Set<SnakeSegmentTag>()
                .Set(new SnakeSegmentIndex {Value = 0})
                .Set(new BodyLength {Value = configFeature.SnakeLength})
                .Set(new Position {Value = GetSnakePosition()})
                .Set(new Rotation {Value = Quaternion.identity})
                .Set(new MovementDirection {Value = Vector2Int.up})
                .Set(new MovementInterval {Value = configFeature.SnakeMovementInterval});
            world.InstantiateView(configFeature.SnakeViewId, snake);
            return snake;
        }

        private void CreateSnakeCamera(int targetEntityId)
        {
            var cameraEntity = world.AddEntity("Camera")
                .Set<CameraTag>()
                .Set(new CameraTarget
                {
                    EntityId = targetEntityId
                });

            var gameplayFeature = world.GetFeature<GameplayFeature>();
            world.AssignView(gameplayFeature.CameraViewId, cameraEntity, DestroyViewBehaviour.LeaveOnScene);
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