using TMPro;
using UnityEngine;
using WebSnake.Components;

namespace WebSnake.UI.Impl
{
    public class HudWindow : UiWindow
    {
        [SerializeField] private TextMeshProUGUI _applesCollectedText;
        [SerializeField] private float _delayAfterGameOver = 2f;

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
        }
    }
}