using ME.ECS;
using ME.ECS.Views;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Features.Config;
using WebSnake.Utils;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public class SnakeSegmentSpawnSystem : ISystem, IAdvanceTick
    {
        private Filter _snakeFilter;
        private Filter _segmentFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _snakeFilter = Filter.Create("SnakeFilter-SnakeSegmentSpawnSystem")
                .With<SnakeTag>()
                .With<Position>()
                .OnChanged<BodyLength>()
                .Push();

            _segmentFilter = Filter.Create("SegmentFilter-SnakeSegmentSpawnSystem")
                .With<SnakeSegmentTag>()
                .With<SnakeSegmentIndex>()
                .Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            var configFeature = world.GetFeature<ConfigFeature>();
            
            foreach (var snake in _snakeFilter)
            {
                var segmentBuffer = PoolList<Entity>.Spawn(100);
                SnakeUtils.GetOrderedSegments(_segmentFilter, segmentBuffer, snake.id);
                if (segmentBuffer.Count == 0)
                {
                    Debug.LogError("You probably forgot to create snake head");
                    continue;
                }

                var bodyLength = snake.Read<BodyLength>();
                for (var idx = segmentBuffer.Count; idx < bodyLength.Value; idx++)
                {
                    var prevSegment = segmentBuffer[idx - 1];
                    var segmentPosition = prevSegment.Has<PreviousPosition>()
                        ? prevSegment.Get<PreviousPosition>().Value
                        : prevSegment.Get<Position>().Value + Vector3.back; // TODO Find empty tile
                    
                    var segmentEntity = world.AddEntity("SnakeSegment")
                        .Set<SnakeSegmentTag>()
                        .Set(new SnakeSegmentIndex {Value = idx})
                        .Set(new ParentId {Value = snake.id})
                        .Set(new Position {Value = segmentPosition});
                    world.InstantiateView(configFeature.SnakeSegmentView, segmentEntity);
                    segmentBuffer.Add(segmentEntity);
                    
                    Debug.Log($"Created ({idx}) snake segment  at ({segmentPosition})");
                }

                PoolList<Entity>.Recycle(segmentBuffer);
            }
        }
    }
}