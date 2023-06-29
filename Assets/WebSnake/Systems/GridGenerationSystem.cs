using ME.ECS;
using WebSnake.Components;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class GridGenerationSystem : ISystem, IUpdate
    {
        public Filter Filter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            Filter.Create("Filter-GridGenerationSystem").With<GenerateGrid>().Push(ref Filter);
        }

        void ISystemBase.OnDeconstruct()
        {
            
        }

        public void Update(in float deltaTime)
        {
            
        }
    }
}