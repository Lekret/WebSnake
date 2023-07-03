using ME.ECS;
using TMPro;
using UnityEngine;
using WebSnake.Components;

namespace WebSnake.UI.Impl
{
    public class HudWindow : UiWindow
    {
        [SerializeField] private TextMeshProUGUI _applesCollectedText;

        private int _prevApplesCollected = -1;

        public override void Tick()
        {
            var applesCollected = World.ReadSharedData<ApplesCollected>();
            if (_prevApplesCollected != applesCollected.Value)
            {
                _prevApplesCollected = applesCollected.Value;
                _applesCollectedText.text = applesCollected.Value.ToString();
            } 

            if (World.HasSharedData<GameOverTag>())
                ChangeWindow<LoadingWindow>();
        }
    }
}