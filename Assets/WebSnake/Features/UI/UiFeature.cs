using System;
using ME.ECS;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Systems;
using WebSnake.UI;
using WebSnake.UI.Impl;
using Object = UnityEngine.Object;

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
        [SerializeField] private UiFade _uiFade;
        [SerializeField] private UiWindow[] _windows = Array.Empty<UiWindow>();

        private Transform _uiRoot;
        
        public UiController Controller { get; private set; }
        public UiFade Fade { get; private set; }

        protected override void OnConstruct()
        {
            _uiRoot = new GameObject("UiRoot").transform;
            Fade = Instantiate(_uiFade, _uiRoot);
            Controller = new UiController(_uiRoot);
            Controller.Init(this, _windows);
            Controller.ChangeWindow<LoadingWindow>(window =>
            {
                window.SetOnTick(() =>
                {
                    if (world.HasSharedData<GameLoadedTag>())
                        Controller.ChangeWindow<HudWindow>();
                });
            });

            AddSystem<UiUpdateSystem>();
        }

        protected override void OnDeconstruct()
        {
            Controller.Dispose();
            if (_uiRoot)
                Destroy(_uiRoot);
        }
    }
}