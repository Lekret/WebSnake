using System.Collections.Generic;
using ME.ECS;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Utils;

namespace WebSnake.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class SnakeRotationSystem : ISystem, IAdvanceTick
    {
        private Filter _snakeFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _snakeFilter = Filter.Create("SnakeFilter-SnakeRotationSystem")
                .With<SnakeTag>()
                .Push();
        }

        void ISystemBase.OnDeconstruct()
        {
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            foreach (var snake in _snakeFilter)
            {
                var segments = PoolList<Entity>.Spawn(100);
                var segmentRotations = PoolList<Quaternion>.Spawn(100);
                SnakeUtils.GetOrderedSnakeSegments(world, snake.id, segments);
                UpdateSegmentsRotation(segments);
                PoolList<Entity>.Recycle(segments);
                PoolList<Quaternion>.Recycle(segmentRotations);
            }
        }

        private void UpdateSegmentsRotation(List<Entity> segments)
        {
            if (segments.Count == 0)
            {
                Debug.LogError("Snake is invalid");
                return;
            }

            for (var i = 0; i < segments.Count; i++)
            {
                var segment = segments[i];
                var segmentRotation = CalculateSegmentRotation(segment);
                var isHead = i == 0;
                var canBeTail = i == segments.Count - 1;
                var canDoSmoothing = !isHead && !canBeTail;
                if (canDoSmoothing)
                {
                    var prevSegmentRotation = CalculateSegmentRotation(segments[i - 1]);
                    if (prevSegmentRotation != segmentRotation)
                    {
                        segmentRotation = Quaternion.Slerp(prevSegmentRotation, segmentRotation, 0.5f);
                    }
                }
                else if (canBeTail && segments.Count > 2)
                {
                    segmentRotation = CalculateSegmentRotation(segments[^2]);
                }

                segment.Get<Rotation>().Value = segmentRotation;
            }
        }

        private static Quaternion CalculateSegmentRotation(Entity segment)
        {
            return Quaternion.LookRotation(segment.Read<MovementDirection>().Value);
        }
    }
}