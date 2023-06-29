using ME.ECS;
using UnityEngine;

namespace WebSnake.Components
{
    public struct MovementDirection : IComponent
    {
        public Vector2 Value;
    }

    public struct BodyLength : IComponent
    {
        public int Value;
    }

    public struct Died : IComponentOneShot
    {
        
    }
}