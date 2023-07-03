using System.Collections.Generic;
using ME.ECS;
using UnityEngine;
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

        public static TileInteractionResult HandleSnakeTileInteraction(World world, Entity snake, Entity tile)
        {
            if (tile.IsEmpty())
            {
                Debug.LogError("Tile is empty");
                return TileInteractionResult.Invalid;
            }

            if (!tile.Has<OccupiedBy>()) 
                return TileInteractionResult.Unoccupied;
            
            var occupantId = tile.Read<OccupiedBy>().Value;
            var occupant = world.GetEntityById(occupantId);
            if (!occupant.IsEmpty())
            {
                if (occupant.Has<SnakeSegmentTag>())
                {
                    snake.Set<DeadTag>();
                    return TileInteractionResult.Dead;
                }

                if (occupant.Has<CollectableTag>())
                {
                    occupant.SetOneShot(new CollectedBy {Value = snake.id});
                    GridUtils.DeoccupyTile(tile);
                    return TileInteractionResult.Collectable;
                }
            }
            
            Debug.LogError($"Tile at {tile.Read<Position>()} is occupied by undefined entity ({occupant})");
            return TileInteractionResult.Invalid;
        }
    }
}