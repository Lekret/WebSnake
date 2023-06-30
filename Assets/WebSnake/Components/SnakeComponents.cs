using ME.ECS;
using UnityEngine;

namespace WebSnake.Components
{
    public struct SpawnSnake : IComponentOneShot
    {
    }

    public struct SnakeTag : IStructComponent
    {
    }

    public struct MovementDirection : IStructComponent
    {
        public Vector3 Value;
    }
    
    public struct Speed : IStructComponent
    {
        public float Value;
    }

    public struct BodyLength : IStructComponent
    {
        public int Value;
    }

    public struct Died : IComponentOneShot
    {
    }
}