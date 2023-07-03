using System;

namespace WebSnake.UI.Impl
{
    public class LoadingWindow : UiWindow
    {
        private Action _onTick;
        private bool _visibleDuringTick = true;
        private float _tickDelay = -1f;
        
        public void SetOnTick(Action onTick) => _onTick = onTick;

        public void SetTickDelay(float tickDelay) => _tickDelay = tickDelay;

        public void SetVisibleDuringTickDelay(bool value) => _visibleDuringTick = value;

        public override void Tick(in float deltaTime)
        {
            var canTick = UpdateCanTick(deltaTime);
            gameObject.SetActive(canTick || _visibleDuringTick);
            if (canTick)
                _onTick?.Invoke();
        }

        private bool UpdateCanTick(float deltaTime)
        {
            if (_tickDelay <= 0)
                return true;

            _tickDelay -= deltaTime;
            return _tickDelay <= 0;
        }
    }
}