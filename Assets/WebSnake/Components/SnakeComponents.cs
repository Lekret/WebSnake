﻿using ME.ECS;
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

    public struct MovementDirection : IStructComponent
    {
        public Vector3 Value;
    }

    public struct OldMovementDirection : IStructComponent
    {
        public Vector3 Value;
    }

    public struct ParentId : IStructComponent
    {
        public int Value;
    }

    public struct SnakeTailTag : IStructComponent, IComponentsTag
    {
    }

    public struct Moved : IComponentOneShot
    {
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

    public struct BodyLengthDirtyTag : IStructComponent, IComponentsTag
    {
    }

    public struct DeadTag : IStructComponent, IComponentsTag, IVersioned
    {
    }
}