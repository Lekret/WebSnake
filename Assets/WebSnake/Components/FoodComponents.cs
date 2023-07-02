using ME.ECS;

namespace WebSnake.Components
{
    public struct AppleTag : IStructComponent, IComponentsTag
    {
    }

    public struct BananaTag : IStructComponent, IComponentsTag
    {
    }

    public struct Nutrition : IStructComponent
    {
        public int Value;
    }

    public struct CollectableTag : IStructComponent, IComponentsTag
    {
    }

    public struct GameStatsChanged : IComponentOneShot
    {
    }

    public struct CollectedBy : IComponentOneShot
    {
        public int Value;
    }
}