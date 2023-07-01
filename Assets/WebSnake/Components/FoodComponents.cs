using ME.ECS;

namespace WebSnake.Components
{
    public struct AppleTag : IStructComponent, IComponentsTag
    {
    }

    public struct BananaTag : IStructComponent, IComponentsTag
    {
    }

    public struct FoodBodyLenghtGain : IStructComponent
    {
        public int Value;
    }
}