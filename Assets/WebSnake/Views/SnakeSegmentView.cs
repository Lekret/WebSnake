using ME.ECS;
using ME.ECS.Views.Providers;
using UnityEngine;
using WebSnake.Components;

namespace WebSnake.Views
{
    public class SnakeSegmentView : MonoBehaviourView
    {
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private Mesh _bodyMesh;
        [SerializeField] private Mesh _tailMesh;
        [SerializeField] private Vector3 _positionOffsetOnRotationSmoothing;

        public override bool applyStateJob => false;
        
        public override void ApplyState(float deltaTime, bool immediately)
        {
            var parentId = entity.Read<ParentId>().Value;
            var head = world.GetEntityById(parentId);
            if (!head.IsEmpty() && head.Has<DeadTag>())
                return;

            var rotation = entity.Read<Rotation>();
            var isTail = entity.Has<SnakeTailTag>();
            transform.position = entity.Read<Position>().Value;
            transform.rotation = rotation.Value;
            _meshFilter.mesh = isTail ? _tailMesh : _bodyMesh;

            if (!isTail)
                ApplyOffsetIfRotationSmoothed(rotation);
        }

        private void ApplyOffsetIfRotationSmoothed(Rotation rotation)
        {
            var moveDirection = entity.Read<MovementDirection>();
            var rotationFwd = rotation.Value * Vector3.forward;
            var rotationSmoothingAngle = Vector3.SignedAngle(moveDirection.Value, rotationFwd, Vector3.up);
            if (Mathf.Approximately(rotationSmoothingAngle, Mathf.Epsilon))
            {
                _meshFilter.transform.localPosition = Vector3.zero;
            }
            else
            {
                _meshFilter.transform.localPosition = _positionOffsetOnRotationSmoothing *
                                                      Mathf.Sign(rotationSmoothingAngle);
            }
        }
    }
}