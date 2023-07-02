using ME.ECS;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Features.SharedFilters;

namespace WebSnake.Utils
{
    public static class GridUtils
    {
        public static void OccupyTile(Entity occupant, Entity tile)
        {
            if (tile.IsEmpty())
            {
                Debug.LogError("Tile is empty entity");
                return;
            }

            if (tile.Has<OccupiedById>())
            {
                var activeOccupant = Worlds.currentWorld.GetEntityById(tile.Read<OccupiedById>().Value);
                Debug.LogError($"Cant occupy by ({occupant}), tile is already occupied by ({activeOccupant})");
                return;
            }

            tile.Get<OccupiedById>().Value = occupant.id;
        }

        public static void OccupyTile(World world, Entity occupant)
        {
            if (occupant.IsEmpty())
            {
                Debug.LogError("Occupant is empty entity");
                return;
            }
            
            var tile = GetTileByOccupant(world, occupant);
            if (tile.IsEmpty())
            {
                Debug.LogError($"Tile not found on position: {occupant.Read<Position>().Value}");
            }
            else
            {
                OccupyTile(occupant, tile);
            }
        }

        public static void DeoccupyTile(World world, Entity occupant)
        {
            if (occupant.IsEmpty())
            {
                Debug.LogError("Occupant is empty entity");
                return;
            }

            var tile = GetTileByOccupant(world, occupant);
            if (tile.IsEmpty())
            {
                Debug.LogError($"Tile not found on position: {occupant.Read<Position>().Value}");
                return;
            }

            if (tile.Has<OccupiedById>())
            {
                tile.Remove<OccupiedById>();
            }
            else
            {
                Debug.LogWarning("Tile is already not occupied, doing nothing");
            }
        }

        public static Entity GetTileByOccupant(World world, Entity occupant)
        {
            var filter = world.GetFeature<SharedFiltersFeature>().GridFilter;
            foreach (var grid in filter)
            {
                var occupantPosition = occupant.Read<Position>().Value;
                var positionToTile = grid.Read<PositionToTile>().Value;
                if (positionToTile.TryGetValue(occupantPosition, out var tileId))
                {
                    return world.GetEntityById(tileId);
                }

                Debug.LogError($"Tile not found on position: {occupantPosition}");
            }

            Debug.LogError("Grid not found");
            return Entity.Empty;
        }
    }
}