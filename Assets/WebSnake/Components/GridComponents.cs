using ME.ECS;

namespace WebSnake.Components
{
    public struct GenerateGrid : IStructComponent, IComponentShared, IComponentOneShot
    {
        public int Width;
        public int Height;
    }

    public struct Grid : IStructComponent, IComponentsTag
    {
        
    }

    public struct GridSize : IStructComponent
    {
        public int Width;
        public int Height;
    }
}