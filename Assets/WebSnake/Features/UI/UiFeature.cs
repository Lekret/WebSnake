using System;
using ME.ECS;
using UnityEngine;
using WebSnake.Systems;
using WebSnake.UI;
using WebSnake.UI.Impl;

namespace WebSnake.Features.UI
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    [CreateAssetMenu(menuName = "Features/" + nameof(UiFeature), fileName = nameof(UiFeature))]
    public class UiFeature : Feature
    {
        [SerializeField] private UiWindow[] _windows = Array.Empty<UiWindow>();

        private UiController _controller;
        
        protected override void OnConstruct()
        {
            AddSystem<UiUpdateSystem>();
            
            _controller = new UiController();
            _controller.Init(world, _windows);
            _controller.ChangeWindow<LoadingWindow>();
        }

        protected override void OnDeconstruct()
        {
            _controller.Dispose();
        }

        public void UpdateUi(in float deltaTime)
        {
            _controller.Tick(deltaTime);
        }
    }
}