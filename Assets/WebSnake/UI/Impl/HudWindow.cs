using ME.ECS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebSnake.Components;
using WebSnake.Markers;
using WebSnake.UI.Utils;

namespace WebSnake.UI.Impl
{
    public class HudWindow : UiWindow
    {
        [SerializeField] private TextMeshProUGUI _applesCollectedText;
        [SerializeField] private float _delayAfterGameOver = 2f;
        [SerializeField] private Toggle _showMobileInputToggle;
        [SerializeField] private MobileInputButton _upButton;
        [SerializeField] private MobileInputButton _downButton;
        [SerializeField] private MobileInputButton _leftButton;
        [SerializeField] private MobileInputButton _rightButton;

        private int _prevApplesCollected = -1;

        protected override void OnInit()
        {
            _applesCollectedText.text = "0";
            _showMobileInputToggle.SetIsOnWithoutNotify(true);
            OnShowMobileInputChanged(true);
            _showMobileInputToggle.onValueChanged.AddListener(OnShowMobileInputChanged);
        }

        public override void Dispose()
        {
            if (_showMobileInputToggle)
                _showMobileInputToggle.onValueChanged.RemoveListener(OnShowMobileInputChanged);
        }

        private void OnShowMobileInputChanged(bool value)
        {
            _upButton.SetVisible(value);
            _downButton.SetVisible(value);
            _leftButton.SetVisible(value);
            _rightButton.SetVisible(value);
        }

        public override void Tick(in float deltaTime)
        {
            var applesCollected = World.ReadSharedData<ApplesCollected>();
            if (_prevApplesCollected != applesCollected.Value)
            {
                _prevApplesCollected = applesCollected.Value;
                _applesCollectedText.text = applesCollected.Value.ToString();
            }

            if (World.HasSharedData<GameOverTag>())
            {
                ChangeWindow<LoadingWindow>(loadingWindow =>
                {
                    loadingWindow.SetTickDelay(_delayAfterGameOver);
                    loadingWindow.SetVisibleDuringTickDelay(false);
                    loadingWindow.SetOnTick(() =>
                    {
                        if (World.HasSharedData<GameOverTag>() && World.HasSharedData<EndGameResponseHolder>())
                            ChangeWindow<GameOverWindow>();
                    });
                });
            }

            UpdateInput();
        }

        private void UpdateInput()
        {
            var direction = Vector2Int.zero;
            if (_upButton.IsPressed) direction.y += 1;
            if (_downButton.IsPressed) direction.y -= 1;
            if (_leftButton.IsPressed) direction.x -= 1;
            if (_rightButton.IsPressed) direction.x += 1;

            if (direction != Vector2Int.zero)
            {
                World.AddMarker(new InputMovementDirection
                {
                    Value = direction
                });
            }
        }
    }
}