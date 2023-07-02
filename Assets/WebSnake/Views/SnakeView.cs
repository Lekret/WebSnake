﻿using ME.ECS;
using ME.ECS.Views.Providers;
using WebSnake.Components;

namespace WebSnake.Views
{
    public class SnakeView : MonoBehaviourView
    {
        public override bool applyStateJob => false;

        public override void ApplyState(float deltaTime, bool immediately)
        {
            if (entity.Has<DeadTag>())
                return;

            transform.position = entity.Read<Position>().Value;
            transform.rotation = entity.Read<Rotation>().Value;
        }
    }
}