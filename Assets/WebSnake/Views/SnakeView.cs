using ME.ECS;

namespace WebSnake.Views {
    
    using ME.ECS.Views.Providers;
    
    public class SnakeView : MonoBehaviourView {
        
        public override bool applyStateJob => false;

        public override void OnInitialize() {
            
        }
        
        public override void OnDeInitialize() {
            
        }
        
        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) {
            
        }
        
        public override void ApplyState(float deltaTime, bool immediately) {
            
        }
        
    }
    
}