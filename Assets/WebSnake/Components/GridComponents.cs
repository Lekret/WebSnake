using ME.ECS;

namespace WebSnake.Components
{
    public struct GenerateGrid : IComponentShared, IComponentOneShot
    {
        public int Width;
        public int Height;
    }

    public struct GridTag : IStructComponent, IComponentsTag
    {
        
    }

    public struct GridSize : IStructComponent
    {
        public int Width;
        public int Height;
    }
}