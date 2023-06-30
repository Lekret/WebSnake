using ME.ECS;
using ME.ECS.Views;
using ME.ECS.Views.Providers;
using UnityEngine;
using WebSnake.Systems;

namespace WebSnake.Features.Gameplay
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    [CreateAssetMenu(menuName = "Features/" + nameof(GameplayFeature), fileName = nameof(GameplayFeature))]
    public sealed class GameplayFeature : Feature
    {
        public GameObject CellPrefab;
        public MonoBehaviourViewBase SnakeView;

        public ViewId SnakeViewId { get; private set; }

        protected override void OnConstruct()
        {
            AddModule<ViewsModule>();

            AddSystem<PlayerInputSystem>();
            AddSystem<CreateGameSystem>();
            AddSystem<GameCreatedSystem>();
            AddSystem<GridGenerationSystem>();
            AddSystem<SnakeSpawnSystem>();
            AddSystem<SnakeMovementSystem>();
            AddSystem<CameraFollowSystem>();
            AddSystem<GameEndedSystem>();
            AddSystem<WebRequestSystem>();

            SnakeViewId = world.RegisterViewSource(SnakeView);
        }

        protected override void OnDeconstruct()
        {
        }
    }
}