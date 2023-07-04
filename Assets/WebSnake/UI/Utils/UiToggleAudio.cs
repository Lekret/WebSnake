using UnityEngine;
using UnityEngine.UI;

namespace WebSnake.UI.Utils
{
    [RequireComponent(typeof(Toggle))]
    public class UiToggleAudio : MonoBehaviour
    {
        [SerializeField] private UiAudioPreset _preset;

        private Toggle _toggle;
        private static AudioSource _sharedAudioSource;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            
            if (!_sharedAudioSource)
            {
                _sharedAudioSource = new GameObject("UiAudioSource").AddComponent<AudioSource>();
                _sharedAudioSource.spatialBlend = 0f;
            }

            if (!_preset)
            {
                Debug.LogError($"UiAudioPreset is null: {name}", this);
                enabled = false;
            }
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnValueChanged);
        }
        
        private void OnValueChanged(bool _)
        {
            _sharedAudioSource.PlayOneShot(_preset.ClickAudio);
        }
    }
}