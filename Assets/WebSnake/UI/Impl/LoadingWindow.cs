using WebSnake.Components;

namespace WebSnake.UI.Impl
{
    public class LoadingWindow : UiWindow
    {
        public override void Tick()
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