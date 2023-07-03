using Cinemachine;
using ME.ECS;
using ME.ECS.Views.Providers;
using UnityEngine;
using WebSnake.Components;

namespace WebSnake.Views
{
    public class SnakeView : MonoBehaviourView
    {
        [SerializeField] private GameObject _deathVfx;
        [SerializeField] private CinemachineImpulseSource _deathImpulseSource;

        private bool _handledDeath;
        
        public override bool applyStateJob => false;

        public override void OnDeInitialize()
        {
            _handledDeath = false;
        }

        public override void ApplyState(float deltaTime, bool immediately)
        {
            if (entity.Has<DeadTag>())
            {
                if (!_handledDeath)
                {
                    Instantiate(_deathVfx, transform.position, Quaternion.identity);
                    _deathImpulseSource.GenerateImpulse();
                    _handledDeath = true;
                }
                
                return;
            }

            transform.position = entity.Read<Position>().Value;
            transform.rotation = entity.Read<Rotation>().Value;
        }
    }
}