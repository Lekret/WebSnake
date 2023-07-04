using UnityEngine;
using UnityEngine.UI;

namespace WebSnake.UI.Utils
{
    [RequireComponent(typeof(Button))]
    public class UiButtonAudio : MonoBehaviour
    {
        [SerializeField] private UiAudioPreset _preset;

        private Button _button;
        private static AudioSource _sharedAudioSource;

        private void Awake()
        {
            _button = GetComponent<Button>();
            
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
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }
        
        private void OnClick()
        {
            _sharedAudioSource.PlayOneShot(_preset.ClickAudio);
        }
    }
}