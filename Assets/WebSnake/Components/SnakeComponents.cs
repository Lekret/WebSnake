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

    public struct SnakeSegmentTag : IStructComponent, IComponentsTag
    {
    }

    public struct SnakeSegmentIndex : IStructComponent
    {
        public int Value;
    }

    public struct PreviousPosition : IStructComponent
    {
        public Vector3 Value;
    }

    public struct ParentId : IStructComponent
    {
        public int Value;
    }

    public struct MovementDirection : IStructComponent
    {
        public Vector3 Value;
    }

    public struct NewMovementDirection : IStructComponent
    {
        public Vector3 Value;
    }

    public struct MovementInterval : IStructComponent
    {
        public float Value;
    }

    public struct MovementIntervalAccum : IStructComponent
    {
        public float Value;
    }

    public struct BodyLength : IStructComponent, IVersioned
    {
        public int Value;
    }

    public struct DeadTag : IStructComponent, IComponentsTag
    {
    }
}