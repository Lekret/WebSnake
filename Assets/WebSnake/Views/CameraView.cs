using Cinemachine;
using ME.ECS.Views.Providers;
using UnityEngine;

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
            
        }
    }
}