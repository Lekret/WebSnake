using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace WebSnake.UI.Utils
{
    public class UiFade : MonoBehaviour
    {
        [SerializeField] private Image _fadeTarget;
        [SerializeField] private float _fadeInTime = 1f;
        [SerializeField] private float _fadeOutTime = 1f;

        public void FadeIn(Action onComplete) => ToAlpha(1, _fadeInTime, onComplete);

        public void FadeOut(Action onComplete) => ToAlpha(0, _fadeOutTime, onComplete);

        private void ToAlpha(float alpha, float duration, Action onComplete)
        {
            DOTween
                .ToAlpha(() => _fadeTarget.color, newColor => _fadeTarget.color = newColor, alpha, duration)
                .OnComplete(() => onComplete?.Invoke());
        }
    }
}