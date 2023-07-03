using WebSnake.Components;

namespace WebSnake.UI.Impl
{
    public class LoadingWindow : UiWindow
    {
        private bool _visibleDuringTransition = true;
        private float _transitionDelay = -1f;

        public void SetTransitionDelay(int delay)
        {
            _transitionDelay = delay;
        }

        public void SetVisibleDuringTransitionDelay(bool value)
        {
            _visibleDuringTransition = value;
        }

        public override void Tick(in float deltaTime)
        {
            UpdateTransitionDelay(deltaTime, out var canTransition);
            gameObject.SetActive(canTransition || _visibleDuringTransition);
            if (canTransition)
                HandleTransitions();
        }

        private void UpdateTransitionDelay(float deltaTime, out bool canTransition)
        {
            if (_transitionDelay <= 0)
            {
                canTransition = true;
                return;
            }

            _transitionDelay -= deltaTime;
            canTransition = _transitionDelay <= 0;
        }
        
        private void HandleTransitions()
        {
            if (World.HasSharedData<GameOverTag>())
            {
                if (World.HasSharedData<EndGameResponseHolder>())
                    ChangeWindow<GameOverWindow>();
            }
            else if (World.HasSharedData<GameLoadedTag>())
            {
                ChangeWindow<HudWindow>();
            }
        }
    }
}