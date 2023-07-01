﻿using ME.ECS;
using ME.ECS.Views;
using ME.ECS.Views.Providers;
using UnityEngine;
using WebSnake.Extensions;
using WebSnake.Systems;

namespace WebSnake.Features.Gameplay
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    [CreateAssetMenu(menuName = "Features/" + nameof(GameplayFeature), fileName = nameof(GameplayFeature))]
    public sealed class GameplayFeature : Feature
    {
        private GameSceneProvider _gameSceneProvider;

        public ViewId CameraViewId { get; private set; }
        public IMonoViewsRepo ViewsRepo { get; private set; }

        protected override void OnConstruct()
        {
            _gameSceneProvider = FindObjectOfType<GameSceneProvider>();
            ViewsRepo = new MonoViewsRepo();
            CameraViewId = world.RegisterViewSource(_gameSceneProvider.CameraView);
            
            AddModule<ViewsModule>();

            AddSystem<CreateGameSystem>();
            AddSystem<GameCreatedSystem>();
            AddSystem<GridGenerationSystem>();
            AddSystem<SnakeSpawnSystem>();
            AddSystem<SnakeHandleInputSystem>();
            AddSystem<SnakeMovementSystem>();
            AddSystem<GameEndedSystem>();
            AddSystem<WebRequestSystem>();
        }

        protected override void OnDeconstruct()
        {
        }
    }
}