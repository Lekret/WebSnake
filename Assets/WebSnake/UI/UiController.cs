using System;
using System.Collections.Generic;
using ME.ECS;
using UnityEngine;
using Object = UnityEngine.Object;

namespace WebSnake.UI
{
    public class UiController : IDisposable
    {
        private readonly List<UiWindow> _windows = new();
        private UiWindow _currentWindow;
        private Transform _windowsRoot;

        public void Init(World world, UiWindow[] windowPrefabs)
        {
            _windowsRoot = new GameObject("UiWindows").transform;
            
            foreach (var windowPrefab in windowPrefabs)
            {
                var windowInstance = Object.Instantiate(windowPrefab, _windowsRoot);
                _windows.Add(windowInstance);
                windowInstance.Init(world, this);
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
                window.Dispose();
            }
            
            if (_windowsRoot)
                Object.Destroy(_windowsRoot.gameObject);
        }
    }
}