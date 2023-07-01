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
        public Vector2Int Value;
    }
    
    public struct NewMovementDirection : IStructComponent
    {
        public Vector2Int Value;
    }

    public struct MovementInterval : IStructComponent
    {
        public float Value;
    }

    public struct MovementIntervalAccum : IStructComponent
    {
        public float Value;
    }

    public struct BodyLength : IStructComponent
    {
        public int Value;
    }

    public struct Dead : IStructComponent, IComponentsTag
    {
    }
}