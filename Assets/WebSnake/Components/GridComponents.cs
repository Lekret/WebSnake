using System.Collections.Generic;
using ME.ECS;
using UnityEngine;

namespace WebSnake.Components
{
    public struct GenerateGrid : IComponentShared, IComponentOneShot
    {
        public int Width;
        public int Height;
    }

    public struct GridTag : IStructComponent, IComponentsTag
    {
    }
    
    public struct GridSize : IStructComponent
    {
        public int Width;
        public int Height;
    }

    public struct PositionToTile : IStructCopyable<PositionToTile>
    {
        public Dictionary<Vector3, int> Value;
        
        public void CopyFrom(in PositionToTile other) => Value = new Dictionary<Vector3, int>(other.Value);

        public void OnRecycle() => Value = null;
    }
    
    public struct GridTileTag : IStructComponent, IComponentsTag
    {
    }

    public struct OccupiedById : IStructComponent
    {
        public int Value;
    }
}