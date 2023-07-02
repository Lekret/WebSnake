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

    public struct CollectedBy : IComponentOneShot
    {
        public int Value;
    }
}