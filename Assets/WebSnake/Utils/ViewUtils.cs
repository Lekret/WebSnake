using ME.ECS;
using ME.ECS.Views;
using ME.ECS.Views.Providers;

namespace WebSnake.Utils
{
    public static class ViewUtils
    {
        public static bool TryGetEntityView(this World world, Entity entity, out MonoBehaviourView view)
        {
            view = world.GetEntityView(entity);
            return view;
        }

        public static bool TryGetEntityView(this World world, int entityId, out MonoBehaviourView view)
        {
            view = world.GetEntityView(entityId);
            return view;
        }

        public static MonoBehaviourView GetEntityView(this World world, int entityId)
        {
            var entity = world.GetEntityById(entityId);
            if (entity.IsEmpty())
                return null;

            return world.GetModule<IViewModule>().GetViewByEntity(entity) as MonoBehaviourView;
        }

        public static MonoBehaviourView GetEntityView(this World world, Entity entity)
        {
            return world.GetModule<IViewModule>().GetViewByEntity(entity) as MonoBehaviourView;
        }
    }
}