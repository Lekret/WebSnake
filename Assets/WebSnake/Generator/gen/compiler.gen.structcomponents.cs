namespace ME.ECS {

    public static partial class ComponentsInitializer {

        static partial void InitTypeIdPartial() {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();



        }

        static partial void Init(State state, ref ME.ECS.World.NoState noState) {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();



            ComponentsInitializerWorld.Setup(ComponentsInitializerWorldGen.Init);
            CoreComponentsInitializer.Init(state, ref noState);



        }

    }

    public static class ComponentsInitializerWorldGen {

        public static void Init(Entity entity) {



        }

    }

}
