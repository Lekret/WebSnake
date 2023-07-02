using System.Collections.Generic;
using ME.ECS;
using WebSnake.Components;
using WebSnake.Features.SharedFilters;

namespace WebSnake.Utils
{
    public static class SnakeUtils
    {
        public static void GetOrderedSnakeSegments(World world, int parentId, List<Entity> buffer)
        {
            var filter = world.GetFeature<SharedFiltersFeature>().SnakeSegmentFilter;
            if (filter.Count == 0)
                return;

            foreach (var entity in filter)
            {
                if (!entity.Has<SnakeSegmentIndex>())
                    continue;

                var isChild = entity.Has<ParentId>() && entity.Read<ParentId>().Value == parentId;
                if (isChild || entity.id == parentId)
                    buffer.Add(entity);
            }

            if (buffer.Count > 1)
                buffer.Sort((x, y) => x.Read<SnakeSegmentIndex>().Value.CompareTo(y.Read<SnakeSegmentIndex>().Value));
        }
    }
}