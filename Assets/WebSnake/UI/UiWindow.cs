using System;
using ME.ECS;
using UnityEngine;

namespace WebSnake.UI
{
    public abstract class UiWindow : MonoBehaviour, IDisposable
    {
        private UiController _uiController;

        protected World World { get; private set; }

        public void Init(World world, UiController uiController)
        {
            World = world;
            _uiController = uiController;
            gameObject.SetActive(false);
            OnInit();
        }

        public virtual void Tick(in float deltaTime)
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

        protected void ChangeWindow<T>(Action<T> beforeChange = null) where T : UiWindow
        {
            _uiController.ChangeWindow(beforeChange);
        }
    }
}