using ME.ECS;
using ME.ECS.Views.Providers;
using WebSnake.Components;

namespace WebSnake.Views
{
    public class AppleView : MonoBehaviourView
    {
        public override bool applyStateJob => false;

        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.Read<Position>().Value;
            transform.rotation = entity.Read<Rotation>().Value;
        }
    }
}