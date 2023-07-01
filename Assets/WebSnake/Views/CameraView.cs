using Cinemachine;
using ME.ECS;
using ME.ECS.Views;
using ME.ECS.Views.Providers;
using UnityEngine;
using WebSnake.Components;
using WebSnake.Extensions;

namespace WebSnake.Views
{
    public class CameraView : MonoBehaviourView
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        
        public override bool applyStateJob => false;

        public override void OnInitialize()
        {
        }

        public override void OnDeInitialize()
        {
        }
        
        public override void ApplyState(float deltaTime, bool immediately)
        {
            if (!entity.Has<CameraTarget>())
            {
                Debug.LogError("Camera with no target, probably not intended");
                return;
            }

            var cameraTarget = entity.Read<CameraTarget>();
            if (world.GetViewsRepo().TryGetView(cameraTarget.EntityId, out var view))
            {
                _virtualCamera.Follow = view.transform;
                _virtualCamera.LookAt = view.transform;
            }
            else
            {
                Debug.LogError("CameraTarget should have valid MonoBehaviourView");
            }
        }
    }
}