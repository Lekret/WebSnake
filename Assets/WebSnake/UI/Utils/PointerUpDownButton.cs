using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WebSnake.UI.Utils
{
    public class PointerUpDownButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private CanvasGroup _alphaTarget;
        [SerializeField] private float _maxAlpha = 0.35f;
        [SerializeField] private float _alphaIncreaseDuration = 0.3f;
        [SerializeField] private float _alphaDecreaseDuration = 0.15f;

        private Tween _activeTween;

        public bool IsPressed { get; private set; }

        private void OnEnable()
        {
            _alphaTarget.alpha = _maxAlpha;
            TweenAlphaTo(0f, 4f);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            IsPressed = true;
            TweenAlphaTo(_maxAlpha, _alphaIncreaseDuration);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            IsPressed = false;
            TweenAlphaTo(0, _alphaDecreaseDuration);
        }

        private void TweenAlphaTo(float alpha, float duration)
        {
            _activeTween?.Kill();
            var alphaDiff = Mathf.Abs(_alphaTarget.alpha + _maxAlpha - alpha);
            _activeTween = DOTween.To(
                () => _alphaTarget.alpha, 
                a => _alphaTarget.alpha = a, 
                alpha, 
                duration * alphaDiff);
        }
    }
}