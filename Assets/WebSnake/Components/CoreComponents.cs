using ME.ECS;
using UnityEngine;

namespace WebSnake.Components
{
    public struct GameId : IStructComponent, IComponentShared
    {
        public int Value;
    }

    public struct GameLoaded : IStructComponent, IComponentShared, IComponentsTag
    {
    }

    public struct GameLaunched : IStructComponent, IComponentShared, IComponentsTag
    {
    }

    public struct CameraTag : IStructComponent, IComponentsTag
    {
        
    }

    public struct CameraTarget : IStructComponent
    {
        public int EntityId;
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