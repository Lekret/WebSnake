using ME.ECS;
using ME.ECS.Views;
using ME.ECS.Views.Providers;
using UnityEngine;

namespace WebSnake.Extensions
{
    /* TODO
    public class CustomViewsProvider : UnityGameObjectProvider
    {
        private readonly IMonoViewsRepo _viewsRepo;

        public CustomViewsProvider(IMonoViewsRepo viewsRepo)
        {
            _viewsRepo = viewsRepo;
            Debug.LogError("OH YEAH!!!");
        }
        
        public override IView Spawn(IView prefab, ViewId prefabSourceId, in Entity targetEntity)
        {
            var view = base.Spawn(prefab, prefabSourceId, in targetEntity);
            if (view is MonoBehaviourView monoView)
                _viewsRepo.Register(monoView, targetEntity);
            return view;
        }

        public override bool Destroy(ref IView instance)
        {
            if (instance is MonoBehaviourView monoView)
                _viewsRepo.Unregister(monoView);
            return base.Destroy(ref instance);
        }
    }
    */
}