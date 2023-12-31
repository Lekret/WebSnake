namespace ME.ECS {

    public static partial class ComponentsInitializer {

        static partial void InitTypeIdPartial() {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<WebSnake.Components.ApplesCollected>(false, true, true, false, false, true, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.BodyLength>(false, true, true, false, false, true, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.CameraTarget>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameId>(false, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GridSize>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.LastBananaSpawnAppleCount>(false, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Lifetime>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.MovementDirection>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.MovementInterval>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.MovementIntervalAccum>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.NewMovementDirection>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Nutrition>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.OccupiedBy>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.OldMovementDirection>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.ParentId>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Position>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Rotation>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SnakeSegmentIndex>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.AppleTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.BananaTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.BodyLengthDirtyTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.CameraTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.CollectableTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.DeadTag>(true, true, true, false, false, true, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameLaunchedTag>(true, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameLoadedTag>(true, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameOverTag>(true, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GridTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GridTileTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.LifetimeExpiredTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SnakeSegmentTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SnakeTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SnakeTailTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.TransformView>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.EndGameResponseHolder>(false, false, false, false, true, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameWebSocketHolder>(false, false, false, false, true, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.PositionToTile>(false, false, false, false, true, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SendRequest>(false, false, false, false, true, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.CollectedBy>(false, false, false, false, false, false, false, false, true);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameStatsChanged>(true, false, false, false, false, false, false, false, true);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GenerateGrid>(false, false, false, false, false, false, false, true, true);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Moved>(true, false, false, false, false, false, false, false, true);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SpawnSnake>(true, false, false, false, false, false, false, true, true);

        }

        static partial void Init(State state, ref ME.ECS.World.NoState noState) {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<WebSnake.Components.ApplesCollected>(false, true, true, false, false, true, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.BodyLength>(false, true, true, false, false, true, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.CameraTarget>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameId>(false, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GridSize>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.LastBananaSpawnAppleCount>(false, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Lifetime>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.MovementDirection>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.MovementInterval>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.MovementIntervalAccum>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.NewMovementDirection>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Nutrition>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.OccupiedBy>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.OldMovementDirection>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.ParentId>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Position>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Rotation>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SnakeSegmentIndex>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.AppleTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.BananaTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.BodyLengthDirtyTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.CameraTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.CollectableTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.DeadTag>(true, true, true, false, false, true, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameLaunchedTag>(true, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameLoadedTag>(true, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameOverTag>(true, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GridTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GridTileTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.LifetimeExpiredTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SnakeSegmentTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SnakeTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SnakeTailTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.TransformView>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.EndGameResponseHolder>(false, false, false, false, true, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameWebSocketHolder>(false, false, false, false, true, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.PositionToTile>(false, false, false, false, true, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SendRequest>(false, false, false, false, true, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.CollectedBy>(false, false, false, false, false, false, false, false, true);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameStatsChanged>(true, false, false, false, false, false, false, false, true);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GenerateGrid>(false, false, false, false, false, false, false, true, true);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Moved>(true, false, false, false, false, false, false, false, true);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SpawnSnake>(true, false, false, false, false, false, false, true, true);

            ComponentsInitializerWorld.Setup(ComponentsInitializerWorldGen.Init);
            CoreComponentsInitializer.Init(state, ref noState);


            state.structComponents.ValidateUnmanaged<WebSnake.Components.ApplesCollected>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.BodyLength>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.CameraTarget>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.GameId>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.GridSize>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.LastBananaSpawnAppleCount>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.Lifetime>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.MovementDirection>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.MovementInterval>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.MovementIntervalAccum>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.NewMovementDirection>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.Nutrition>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.OccupiedBy>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.OldMovementDirection>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.ParentId>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.Position>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.Rotation>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.SnakeSegmentIndex>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.AppleTag>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.BananaTag>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.BodyLengthDirtyTag>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.CameraTag>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.CollectableTag>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.DeadTag>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.GameLaunchedTag>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.GameLoadedTag>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.GameOverTag>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.GridTag>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.GridTileTag>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.LifetimeExpiredTag>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.SnakeSegmentTag>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.SnakeTag>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.SnakeTailTag>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.TransformView>(ref state.allocator, true);
            state.structComponents.ValidateCopyable<WebSnake.Components.EndGameResponseHolder>(false);
            state.structComponents.ValidateCopyable<WebSnake.Components.GameWebSocketHolder>(false);
            state.structComponents.ValidateCopyable<WebSnake.Components.PositionToTile>(false);
            state.structComponents.ValidateCopyable<WebSnake.Components.SendRequest>(false);
            noState.storage.ValidateOneShot<WebSnake.Components.CollectedBy>(false);
            noState.storage.ValidateOneShot<WebSnake.Components.GameStatsChanged>(true);
            noState.storage.ValidateOneShot<WebSnake.Components.GenerateGrid>(false);
            noState.storage.ValidateOneShot<WebSnake.Components.Moved>(true);
            noState.storage.ValidateOneShot<WebSnake.Components.SpawnSnake>(true);

        }

    }

    public static class ComponentsInitializerWorldGen {

        public static void Init(Entity entity) {


            entity.ValidateDataUnmanaged<WebSnake.Components.ApplesCollected>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.BodyLength>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.CameraTarget>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.GameId>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.GridSize>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.LastBananaSpawnAppleCount>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.Lifetime>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.MovementDirection>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.MovementInterval>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.MovementIntervalAccum>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.NewMovementDirection>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.Nutrition>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.OccupiedBy>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.OldMovementDirection>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.ParentId>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.Position>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.Rotation>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.SnakeSegmentIndex>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.AppleTag>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.BananaTag>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.BodyLengthDirtyTag>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.CameraTag>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.CollectableTag>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.DeadTag>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.GameLaunchedTag>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.GameLoadedTag>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.GameOverTag>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.GridTag>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.GridTileTag>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.LifetimeExpiredTag>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.SnakeSegmentTag>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.SnakeTag>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.SnakeTailTag>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.TransformView>(true);
            entity.ValidateDataCopyable<WebSnake.Components.EndGameResponseHolder>(false);
            entity.ValidateDataCopyable<WebSnake.Components.GameWebSocketHolder>(false);
            entity.ValidateDataCopyable<WebSnake.Components.PositionToTile>(false);
            entity.ValidateDataCopyable<WebSnake.Components.SendRequest>(false);
            entity.ValidateDataOneShot<WebSnake.Components.CollectedBy>(false);
            entity.ValidateDataOneShot<WebSnake.Components.GameStatsChanged>(true);
            entity.ValidateDataOneShot<WebSnake.Components.GenerateGrid>(false);
            entity.ValidateDataOneShot<WebSnake.Components.Moved>(true);
            entity.ValidateDataOneShot<WebSnake.Components.SpawnSnake>(true);

        }

    }

}
