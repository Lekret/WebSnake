using System.Collections.Generic;
using ME.ECS;
using ME.ECS.Views.Providers;

namespace WebSnake.Extensions
{
    public class MonoViewsRepo : IMonoViewsRepo
    {
        private readonly Dictionary<int, MonoBehaviourView> _entityToView = new();
        private readonly Dictionary<MonoBehaviourView, int> _viewToEntity = new();

        public MonoBehaviourView GetView(int entity)
        {
            return _entityToView[entity];
        }

        public bool TryGetView(int entityId, out MonoBehaviourView view)
        {
            return _entityToView.TryGetValue(entityId, out view);
        }

        public void RegisterView(MonoBehaviourView view, int entityId)
        {
            _entityToView.Add(entityId, view);
            _viewToEntity.Add(view, entityId);
        }

        public void UnregisterView(MonoBehaviourView view)
        {
            if (_viewToEntity.Remove(view, out var entity))
            {
                _entityToView.Remove(entity);
            }
        }
    }
}