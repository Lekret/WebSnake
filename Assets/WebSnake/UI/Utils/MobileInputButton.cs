using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WebSnake.UI.Utils
{
    public class MobileInputButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private CanvasGroup _alphaTarget;
        [SerializeField] private float _maxAlpha = 0.35f;
        [SerializeField] private float _visibleAlphaIncreaseDuration = 0.4f;
        [SerializeField] private float _invisibleAlphaDecreaseDuration = 0.4f;

        private Tween _activeTween;
        private bool _isVisible;

        public bool IsPressed { get; private set; }

        private void Awake()
        {
            _alphaTarget.alpha = _maxAlpha;
            _isVisible = true;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData) => IsPressed = _isVisible;

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData) => IsPressed = false;

        public void SetVisible(bool value)
        {
            _isVisible = value;
            
            if (value)
                TweenAlphaTo(_maxAlpha, _visibleAlphaIncreaseDuration);
            else
                TweenAlphaTo(0, _invisibleAlphaDecreaseDuration);
        }

        private void TweenAlphaTo(float alpha, float duration)
        {
            _activeTween?.Kill();
            _activeTween = DOTween.To(
                () => _alphaTarget.alpha, 
                a => _alphaTarget.alpha = a, 
                alpha,
                duration);
        }
    }
}