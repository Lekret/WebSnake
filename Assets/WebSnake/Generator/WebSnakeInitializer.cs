using System;
using UnityEngine;
using WebSnake.Features;

#region Namespaces

namespace WebSnake.Generator.Systems { }
namespace WebSnake.Generator.Components { }
namespace WebSnake.Generator.Modules { }
namespace WebSnake.Generator.Features { }
namespace WebSnake.Generator.Markers { }
namespace WebSnake.Generator.Views { }

#endregion

namespace WebSnake.Generator
{
    using TState = WebSnakeState;
    using WebSnake.Modules;
    using ME.ECS;
    using ME.ECS.Views.Providers;
    using WebSnake.Generator.Modules;

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif

    [DefaultExecutionOrder(-1000)]
    public sealed class WebSnakeInitializer : InitializerBase
    {
        private World world;
        public float tickTime = 0.033f;
        public uint inputTicks = 3;
        public int entitiesCapacity = 200;

        public void OnDrawGizmos()
        {
            if (world != null)
            {
                world.OnDrawGizmos();
            }
        }

        public void Update()
        {
            if (world == null)
            {
                WorldUtilities.CreateWorld<TState>(ref world, tickTime);
                {
#if FPS_MODULE_SUPPORT
                    world.AddModule<FPSModule>();
#endif
                    world.AddModule<StatesHistoryModule>();
                    world.GetModule<StatesHistoryModule>().SetTicksForInput(inputTicks);
                    world.AddModule<NetworkModule>();
                    world.SetState<TState>(WorldUtilities.CreateState<TState>());
                    world.SetSeed(1u);
                    ComponentsInitializer.DoInit();
                    world.SetEntitiesCapacity(entitiesCapacity);
                    Initialize(world);
                }
            }

            if (world != null && world.IsLoading() == false && world.IsLoaded() == false)
            {
                world.SetWorldThread(System.Threading.Thread.CurrentThread);
                world.Load(() => { world.SaveResetState<TState>(); });
            }

            if (world != null && world.IsLoaded())
            {
                var dt = Time.deltaTime;
                world.PreUpdate(dt);
                world.Update(dt);
            }
        }

        public void LateUpdate()
        {
            if (world != null && world.IsLoaded() == true) 
                world.LateUpdate(Time.deltaTime);
        }

        public void OnDestroy()
        {
            if (world == null || world.isActive == false) 
                return;

            DeInitializeFeatures(world);
            WorldUtilities.ReleaseWorld<TState>(ref world);
        }
    }
}

namespace ME.ECS
{
    public static partial class ComponentsInitializer
    {
        public static void InitTypeId()
        {
            InitTypeIdPartial();
        }

        static partial void InitTypeIdPartial();

        public static void DoInit()
        {
            Init(Worlds.current.GetState(), ref Worlds.currentWorld.GetNoStateData());
        }

        static partial void Init(State state, ref World.NoState noState);
    }
}