using ME.ECS;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Features.SharedFilters;

namespace WebSnake.Utils
{
    public static class GridUtils
    {
        public static void OccupyTile(World world, Entity occupant)
        {
            if (occupant.IsEmpty())
            {
                Debug.LogError("Occupant is empty entity");
                return;
            }
            
            var occupantPosition = occupant.Read<Position>().Value;
            var tile = GetTileAtPosition(world, occupantPosition);
            if (tile.IsEmpty())
            {
                Debug.LogError($"Tile not found on position: {occupantPosition}");
            }
            else
            {
                OccupyTile(tile, occupant);
            }
        }

        public static void DeoccupyTile(World world, Entity occupant)
        {
            if (occupant.IsEmpty())
            {
                Debug.LogError("Occupant is empty entity");
                return;
            }

            var occupantPosition = occupant.Read<Position>().Value;
            var tile = GetTileAtPosition(world, occupantPosition);
            if (tile.IsEmpty())
            {
                Debug.LogError($"Tile not found on position: {occupantPosition}");
            }
            else
            {
                DeoccupyTile(tile);
            }
        }

        public static void OccupyTile(Entity tile, Entity occupant)
        {
            if (tile.IsEmpty())
            {
                Debug.LogError("Tile is empty entity");
                return;
            }

            if (tile.Has<OccupiedBy>())
            {
                var activeOccupant = Worlds.currentWorld.GetEntityById(tile.Read<OccupiedBy>().Value);
                Debug.LogError($"Cant occupy by ({occupant}), tile is already occupied by ({activeOccupant})");
                return;
            }

            tile.Get<OccupiedBy>().Value = occupant.id;
        }

        public static void DeoccupyTile(Entity tile)
        {
            if (tile.IsEmpty())
            {
                Debug.LogError("Tile is empty");
                return;
            }

            if (tile.Has<OccupiedBy>())
            {
                tile.Remove<OccupiedBy>();
            }
            else
            {
                Debug.LogWarning("Tile is already not occupied, doing nothing");
            }
        }

        public static Entity GetTileAtPosition(World world, Vector3 position)
        {
            var filter = world.GetFeature<SharedFiltersFeature>().GridFilter;
            foreach (var grid in filter)
            {
                var positionToTile = grid.Read<PositionToTile>().Value;
                if (positionToTile.TryGetValue(position, out var tileId))
                    return world.GetEntityById(tileId);

                return Entity.Empty;
            }

            Debug.LogError("Grid not found");
            return Entity.Empty;
        }

        public static void TryTeleport(World world, ref Vector3 position)
        {
            var filter = world.GetFeature<SharedFiltersFeature>().GridFilter;
            foreach (var grid in filter)
            {
                var gridSize = grid.Read<GridSize>();

                if (position.z < 0)
                    position.z = gridSize.Height - 1;
                else if (position.z >= gridSize.Height)
                    position.z = 0;
                else if (position.x < 0)
                    position.x = gridSize.Width - 1;
                else if (position.x >= gridSize.Width)
                    position.x = 0;
            }
        }
    }
}