using System.Collections.Generic;
using ME.ECS;
using UnityEngine;
using WebSnake.Systems;

namespace WebSnake.Features
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    [CreateAssetMenu(menuName = "Features/" + nameof(GameplayFeature), fileName = nameof(GameplayFeature))]
    public sealed class GameplayFeature : Feature
    {
        protected override void OnConstruct()
        {
            AddSystem<StartupSystem>();
            AddSystem<WebResponseSystem>();
            AddSystem<GridGenerationSystem>();
            AddSystem<SnakeMovementSystem>();
            AddSystem<WebRequestSystem>();
        }

        protected override void OnDeconstruct()
        {
        }
    }
}