using ME.ECS;
using ME.ECS.Views.Providers;
using WebSnake.Components;
using WebSnake.Extensions;

namespace WebSnake.Views
{
    public class SnakeView : MonoBehaviourView
    {
        public override bool applyStateJob => false;
        
        public override void OnInitialize()
        {
            world.GetViewsRepo().RegisterView(this, entity.id);
        }

        public override void OnDeInitialize()
        {
            world.GetViewsRepo().UnregisterView(this);
        }
        
        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.Read<Position>().Value;
            transform.rotation = entity.Read<Rotation>().Value;
        }
    }
}