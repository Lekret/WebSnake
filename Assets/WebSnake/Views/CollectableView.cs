using ME.ECS;
using ME.ECS.Views.Providers;
using UnityEngine;
using WebSnake.Components;

namespace WebSnake.Views
{
    public class CollectableView : MonoBehaviourView
    {
        [SerializeField] private GameObject _spawnVfx;
        [SerializeField] private GameObject _collectedVfx;
        
        public override bool applyStateJob => false;

        public override void OnInitialize()
        {
            Instantiate(_spawnVfx, entity.Read<Position>().Value, Quaternion.identity);
        }

        public override void OnDeInitialize()
        {
            Instantiate(_collectedVfx, transform.position, Quaternion.identity);
        }

        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.Read<Position>().Value;
            transform.rotation = entity.Read<Rotation>().Value;
        }
    }
}