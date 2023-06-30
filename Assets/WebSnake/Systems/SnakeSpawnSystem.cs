using ME.ECS;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Features;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public class SnakeSpawnSystem : ISystem, IAdvanceTick
    {
        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (!world.HasSharedDataOneShot<SpawnSnake>())
                return;

            var gameplayFeature = world.GetFeature<GameplayFeature>();
            if (!gameplayFeature)
                return;

            var snakeEntity = world.AddEntity("Snake")
                .Set<SnakeTag>()
                .Set<Position>()
                .Set<Rotation>()
                .Set(new MovementDirection {Value = Vector3.forward})
                .Set(new Speed {Value = 2.0f});
            world.InstantiateView(gameplayFeature.SnakeViewId, snakeEntity);
        }
    }
}