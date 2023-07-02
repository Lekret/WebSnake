using ME.ECS;
using WebSnake.Components;

namespace WebSnake.Systems
{
    public class DestroySystem : ISystem, IAdvanceTickPost
    {
        private Filter _filter;

        public World world { get; set; }
        
        void ISystemBase.OnConstruct()
        {
            _filter = Filter.Create("DestroyFilter-DestroySystem").With<DestroyedTag>().Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTickPost.AdvanceTickPost(in float deltaTime)
        {
            foreach (var entity in _filter)
            {
                entity.Destroy();
            }
        }
    }
}