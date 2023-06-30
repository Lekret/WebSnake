using ME.ECS;
using UnityEngine;

namespace WebSnake.Components
{
    public struct MovementDirectionInput : IComponentOneShot
    {
        public Vector2 Value;
    }
}