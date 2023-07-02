using ME.ECS;
using ME.ECS.Views.Providers;
using UnityEngine;
using WebSnake.Components;

namespace WebSnake.Views
{
    public class SnakeView : MonoBehaviourView
    {
        [SerializeField] private GameObject _deathVfx;

        private bool _spawnedDeathVfx;
        
        public override bool applyStateJob => false;

        public override void OnDeInitialize()
        {
            _spawnedDeathVfx = false;
        }

        public override void ApplyState(float deltaTime, bool immediately)
        {
            if (entity.Has<DeadTag>())
            {
                if (!_spawnedDeathVfx)
                {
                    Instantiate(_deathVfx, transform.position, Quaternion.identity);
                    _spawnedDeathVfx = true;
                }
                
                return;
            }

            transform.position = entity.Read<Position>().Value;
            transform.rotation = entity.Read<Rotation>().Value;
        }
    }
}