using ME.ECS;
using Unity.Collections;

namespace WebSnake.Utils
{
    public static class WorldUtils
    {
        public static Entity GetRandomEntity(this Filter filter)
        {
            if (filter.Count <= 0)
                return Entity.Empty;
            
            var array = filter.ToArray(Allocator.Temp);
            var randomIndex = filter.world.GetRandomRange(0, array.Length);
            return array[randomIndex];
        }
    }
}