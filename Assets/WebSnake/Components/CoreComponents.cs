using ME.ECS;
using UnityEngine;

namespace WebSnake.Components
{
    public struct GameId : IStructComponent, IComponentShared
    {
        public int Value;
    }

    public struct GameLoadedTag : IStructComponent, IComponentShared, IComponentsTag
    {
    }

    public struct GameLaunchedTag : IStructComponent, IComponentShared, IComponentsTag
    {
    }

    public struct GameOverTag : IStructComponent, IComponentShared, IComponentsTag
    {
    }

    public struct CameraTag : IStructComponent, IComponentsTag
    {
    }

    public struct CameraTarget : IStructComponent
    {
        public int EntityId;
    }

    public struct ApplesCollected : IStructComponent, IComponentShared, IVersioned
    {
        public int Value;
    }

    public struct Lifetime : IStructComponent
    {
        public float Value;
    }

    public struct LifetimeExpiredTag : IStructComponent, IComponentsTag
    {
    }

    public struct Position : IStructComponent
    {
        public Vector3 Value;
    }

    public struct Rotation : IStructComponent
    {
        public Quaternion Value;
    }

    public struct TransformView : IStructComponent
    {
    }
}