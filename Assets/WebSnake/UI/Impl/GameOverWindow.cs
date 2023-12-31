﻿using System;
using DG.Tweening;
using ME.ECS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebSnake.Components;
using WebSnake.Markers;

namespace WebSnake.UI.Impl
{
    public class GameOverWindow : UiWindowExtended
    {
        [Header("References")] [SerializeField]
        private Button _restartButton;

        [SerializeField] private TextMeshProUGUI _playtimeText;
        [SerializeField] private TextMeshProUGUI _applesCollectedText;
        [SerializeField] private TextMeshProUGUI _snakeLengthText;
        [SerializeField] private TextMeshProUGUI _gameIdText;

        [Header("Animations")] [SerializeField]
        private RectTransform _animationRoot;

        [SerializeField] private float _appearSpeed = 0.7f;

        protected override void OnInit()
        {
            _restartButton.onClick.AddListener(OnRestartPressed);
        }

        public override void Dispose()
        {
            if (_restartButton)
                _restartButton.onClick.RemoveListener(OnRestartPressed);
        }

        private void OnRestartPressed()
        {
            _restartButton.onClick.RemoveListener(OnRestartPressed);
            Fade.FadeIn(() => World.AddMarker(new Restart()));
        }

        protected override void OnShow()
        {
            if (!World.HasSharedData<EndGameResponseHolder>())
            {
                Debug.LogError("No end game response");
                return;
            }

            var appearFromPosition = new Vector3(0f, RectTransform.sizeDelta.y, 0f);
            _animationRoot.localPosition = appearFromPosition;
            _animationRoot
                .DOLocalMove(Vector3.zero, _appearSpeed)
                .SetEase(Ease.OutQuint);

            var endGameResponse = World.ReadSharedData<EndGameResponseHolder>();
            var payload = endGameResponse.Value.Payload;
            _gameIdText.text = payload.Id.ToString();
            var startAtTime = DateTime.UnixEpoch.AddMilliseconds(long.Parse(payload.FinishAt));
            var finishAtTime = DateTime.UnixEpoch.AddMilliseconds(long.Parse(payload.StartAt));
            var playTime = finishAtTime.Subtract(startAtTime);
            _playtimeText.text = playTime.ToString("mm':'ss");
            _applesCollectedText.text = payload.CollectedApples.ToString();
            _snakeLengthText.text = payload.SnakeLength.ToString();
        }
    }
}