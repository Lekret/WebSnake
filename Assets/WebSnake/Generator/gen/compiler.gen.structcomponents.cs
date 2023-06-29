namespace ME.ECS {

    public static partial class ComponentsInitializer {

        static partial void InitTypeIdPartial() {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<WebSnake.Components.BodyLength>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.CollectedApplesCount>(false, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameId>(false, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GridSize>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.MovementDirection>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Position>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Rotation>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameLaunched>(true, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameLoaded>(true, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Grid>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SnakeTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameWebSocket>(false, false, false, false, true, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SendRequest>(false, false, false, false, true, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Died>(true, false, false, false, false, false, false, false, true);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GenerateGrid>(false, false, false, false, false, false, false, true, true);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SpawnSnake>(true, false, false, false, false, false, false, true, true);

        }

        static partial void Init(State state, ref ME.ECS.World.NoState noState) {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<WebSnake.Components.BodyLength>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.CollectedApplesCount>(false, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameId>(false, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GridSize>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.MovementDirection>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Position>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Rotation>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameLaunched>(true, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameLoaded>(true, true, true, false, false, false, false, true, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Grid>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SnakeTag>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GameWebSocket>(false, false, false, false, true, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SendRequest>(false, false, false, false, true, false, false, false, false);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.Died>(true, false, false, false, false, false, false, false, true);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.GenerateGrid>(false, false, false, false, false, false, false, true, true);
            WorldUtilities.InitComponentTypeId<WebSnake.Components.SpawnSnake>(true, false, false, false, false, false, false, true, true);

            ComponentsInitializerWorld.Setup(ComponentsInitializerWorldGen.Init);
            CoreComponentsInitializer.Init(state, ref noState);


            state.structComponents.ValidateUnmanaged<WebSnake.Components.BodyLength>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.CollectedApplesCount>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.GameId>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.GridSize>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.MovementDirection>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.Position>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.Rotation>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.GameLaunched>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.GameLoaded>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.Grid>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<WebSnake.Components.SnakeTag>(ref state.allocator, true);
            state.structComponents.ValidateCopyable<WebSnake.Components.GameWebSocket>(false);
            state.structComponents.ValidateCopyable<WebSnake.Components.SendRequest>(false);
            noState.storage.ValidateOneShot<WebSnake.Components.Died>(true);
            noState.storage.ValidateOneShot<WebSnake.Components.GenerateGrid>(false);
            noState.storage.ValidateOneShot<WebSnake.Components.SpawnSnake>(true);

        }

    }

    public static class ComponentsInitializerWorldGen {

        public static void Init(Entity entity) {


            entity.ValidateDataUnmanaged<WebSnake.Components.BodyLength>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.CollectedApplesCount>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.GameId>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.GridSize>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.MovementDirection>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.Position>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.Rotation>(false);
            entity.ValidateDataUnmanaged<WebSnake.Components.GameLaunched>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.GameLoaded>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.Grid>(true);
            entity.ValidateDataUnmanaged<WebSnake.Components.SnakeTag>(true);
            entity.ValidateDataCopyable<WebSnake.Components.GameWebSocket>(false);
            entity.ValidateDataCopyable<WebSnake.Components.SendRequest>(false);
            entity.ValidateDataOneShot<WebSnake.Components.Died>(true);
            entity.ValidateDataOneShot<WebSnake.Components.GenerateGrid>(false);
            entity.ValidateDataOneShot<WebSnake.Components.SpawnSnake>(true);

        }

    }

}