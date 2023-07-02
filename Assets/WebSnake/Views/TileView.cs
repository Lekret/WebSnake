using ME.ECS;
using ME.ECS.Views.Providers;
using UnityEngine;
using WebSnake.Components;

namespace WebSnake.Views
{
    public class TileView : MonoBehaviourView
    {
        [SerializeField] private MeshRenderer _renderer;
        [Header("Random Tiling")]
        [SerializeField] private Vector2 _materialTexScale = new(1f / 16f, 1f / 16f);

        [SerializeField] private Vector2Int _minScaledTexOffset = new(0, 0);
        [SerializeField] private Vector2Int _maxScaledTexOffset = new(8, 16);

#if UNITY_EDITOR
        private static Transform _tileParent;
#endif
        
        public override void OnInitialize()
        {
            #if UNITY_EDITOR
            if (!_tileParent)
                _tileParent = new GameObject("Tiles").transform;
            GetComponent<Transform>().SetParent(_tileParent);
            #endif

            transform.position = entity.Read<Position>().Value;
            var material = _renderer.material;
            material.mainTextureScale = _materialTexScale;

            var offset = Vector2.zero;
            for (var i = 0; i < 2; i++)
            {
                offset[i] = _materialTexScale[i] * Random.Range(_minScaledTexOffset[i], _maxScaledTexOffset[i]);
            }
            
            material.mainTextureOffset = offset;
        }
    }
}