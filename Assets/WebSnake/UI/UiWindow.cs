using System;
using ME.ECS;
using UnityEngine;

namespace WebSnake.UI
{
    public abstract class UiWindow : MonoBehaviour, IDisposable
    {
        private Action<Type> _changeWindow;

        protected World World { get; private set; }

        public void Init(World world, Action<Type> changeWindow)
        {
            World = world;
            _changeWindow = changeWindow;
            gameObject.SetActive(false);
            OnInit();
        }

        public virtual void Tick()
        {
        }
        
        public virtual void Dispose()
        {
        }

        public void Show()
        {
            gameObject.SetActive(true);
            OnShow();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            OnHide();
        }

        protected virtual void OnInit()
        {
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }

        protected void ChangeWindow<T>() where T : UiWindow
        {
            _changeWindow.Invoke(typeof(T));
        }
    }
}