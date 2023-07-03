using System;
using System.Collections.Generic;
using ME.ECS;
using UnityEngine;
using WebSnake.Features.UI;
using Object = UnityEngine.Object;

namespace WebSnake.UI
{
    public class UiController : IDisposable
    {
        private readonly List<UiWindow> _windows = new();
        private UiWindow _currentWindow;
        private readonly Transform _uiRoot;

        public UiController(Transform uiRoot)
        {
            _uiRoot = uiRoot;
        }
        
        public void Init(UiFeature uiFeature, UiWindow[] windowPrefabs)
        {
            foreach (var windowPrefab in windowPrefabs)
            {
                var windowInstance = Object.Instantiate(windowPrefab, _uiRoot);
                _windows.Add(windowInstance);
                windowInstance.Init(uiFeature);
            }
        }

        public void Tick(in float deltaTime)
        {
            _currentWindow.Tick(deltaTime);
        }
        
        public void ChangeWindow<T>(Action<T> beforeChange = null) where T : UiWindow
        {
            foreach (var window in _windows)
            {
                if (window is T concreteWindow)
                {
                    if (_currentWindow)
                        _currentWindow.Hide();
                    
                    _currentWindow = concreteWindow;
                    beforeChange?.Invoke(concreteWindow);
                    _currentWindow.Show();
                    return;
                }
            }

            Debug.LogError($"Window of type {typeof(T)} not found");
        }

        public void Dispose()
        {
            foreach (var window in _windows)
            {
                if (window)
                {
                    window.Dispose();
                    Object.Destroy(window);
                }
            }
            
            if (_uiRoot)
                Object.Destroy(_uiRoot.gameObject);
        }
    }
}