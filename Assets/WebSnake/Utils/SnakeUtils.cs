using System.Collections.Generic;
using ME.ECS;
using WebSnake.Components;

namespace WebSnake.Utils
{
    public static class SnakeUtils
    {
        public static void GetOrderedSegments(Filter filter, List<Entity> buffer, int parentId)
        {
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