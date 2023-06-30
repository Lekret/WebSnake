using ME.ECS;
using UnityEngine;

namespace WebSnake.Components
{
    public struct GameId : IStructComponent, IComponentShared
    {
        public int Value;
    }

    public struct GameLoaded : IStructComponent, IComponentShared
    {
    }

    public struct GameLaunched : IStructComponent, IComponentShared
    {
    }

    public struct CollectedApplesCount : IStructComponent, IComponentShared
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