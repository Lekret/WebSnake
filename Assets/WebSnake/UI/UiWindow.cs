using System;
using ME.ECS;
using UnityEngine;
using WebSnake.Features.UI;

namespace WebSnake.UI
{
    public abstract class UiWindow : MonoBehaviour, IDisposable
    {
        private UiController _uiController;
        
        protected RectTransform RectTransform { get; private set; }
        protected World World { get; private set; }

        public virtual void Init(UiFeature uiFeature)
        {
            _uiController = uiFeature.Controller;
            World = uiFeature.world;
            RectTransform = (RectTransform) transform;
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