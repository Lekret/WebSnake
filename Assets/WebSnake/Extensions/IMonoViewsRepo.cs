using ME.ECS;
using ME.ECS.Views.Providers;

namespace WebSnake.Extensions
{
    public interface IMonoViewsRepo
    {
        MonoBehaviourView GetView(int entityId);
        bool TryGetView(int entityId, out MonoBehaviourView view);
        void RegisterView(MonoBehaviourView view, int entityId);
        void UnregisterView(MonoBehaviourView view);
    }
}