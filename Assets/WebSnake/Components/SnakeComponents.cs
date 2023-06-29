using ME.ECS;
using UnityEngine;

namespace WebSnake.Components
{
    public struct SpawnSnake : IComponentShared, IComponentOneShot
    {
    }

    public struct SnakeTag : IStructComponent
    {
    }

    public struct MovementDirection : IStructComponent
    {
        public Vector2 Value;
    }

    public struct BodyLength : IStructComponent
    {
        public int Value;
    }

    public struct Died : IComponentOneShot
    {
    }
}