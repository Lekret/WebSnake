using ME.ECS;
using TMPro;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Markers;
using WebSnake.UI.Utils;

namespace WebSnake.UI.Impl
{
    public class HudWindow : UiWindow
    {
        [SerializeField] private TextMeshProUGUI _applesCollectedText;
        [SerializeField] private float _delayAfterGameOver = 2f;
        [SerializeField] private PointerUpDownButton _upButton;
        [SerializeField] private PointerUpDownButton _downButton;
        [SerializeField] private PointerUpDownButton _leftButton;
        [SerializeField] private PointerUpDownButton _rightButton;

        private int _prevApplesCollected = -1;

        protected override void OnInit()
        {
            _applesCollectedText.text = "0";
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