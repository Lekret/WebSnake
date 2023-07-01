using ME.ECS;
using WebSnake.Features.Input;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class PlayerInputCleanupSystem : ISystem, IAdvanceTickPost
    {
        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTickPost.AdvanceTickPost(in float deltaTime)
        {
            var inputFeature = world.GetFeature<InputFeature>();
            if (inputFeature)
            {
                inputFeature.InputData = default;
            }
        }
    }
}