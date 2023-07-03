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
                windowInstance.Init(world, ChangeWindow);
            }
        }

        public void Tick()
        {
            _currentWindow.Tick();
        }
        
        public void ChangeWindow<T>() where T : UiWindow
        {
            ChangeWindow(typeof(T));
        }

        private void ChangeWindow(Type type)
        {
            foreach (var window in _windows)
            {
                if (type.IsInstanceOfType(window))
                {
                    if (_currentWindow)
                        _currentWindow.Hide();
                    
                    _currentWindow = window;
                    _currentWindow.Show();
                    return;
                }
            }

            Debug.LogError($"Window of type {type} not found");
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