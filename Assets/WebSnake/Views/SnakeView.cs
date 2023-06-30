using ME.ECS;
using UnityEngine;
using WebSnake.Components;

namespace WebSnake.Views
{
    using ME.ECS.Views.Providers;

    public class SnakeView : MonoBehaviourView
    {
        public override bool applyStateJob => false;
        
        public override void OnInitialize()
        {
            Debug.Log($"Snake View Created: {entity}");
        }

        public override void OnDeInitialize()
        {
        }
        
        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.Read<Position>().Value;
            transform.rotation = entity.Read<Rotation>().Value;
        }
    }
}