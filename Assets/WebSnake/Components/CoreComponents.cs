﻿using ME.ECS;
using UnityEngine;

namespace WebSnake.Components
{
    public struct GameId : IComponentShared
    {
        public int Value;
    }

    public struct GameLoaded : IStructComponent, IComponentShared
    {
    }

    public struct GameLaunched : IStructComponent, IComponentShared
    {
    }

    public struct CameraTag
    {
    }

    public struct CollectedApplesCount : IComponentShared
    {
        public int Value;
    }

    public struct Position : IStructComponent
    {
        public Vector3 Value;
    }

    public struct Rotation : IStructComponent
    {
        public Quaternion Value;
    }
}