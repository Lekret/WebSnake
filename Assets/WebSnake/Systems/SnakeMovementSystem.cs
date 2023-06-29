using ME.ECS;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class SnakeMovementSystem : ISystem, IUpdate
    {
        public Filter Filter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            Filter = Filter.Create("Filter-SnakeMovementSystem").Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        public void Update(in float deltaTime)
        {
            
        }
    }
}