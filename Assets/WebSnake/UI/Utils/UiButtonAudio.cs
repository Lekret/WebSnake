using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WebSnake.UI.Utils
{
    [RequireComponent(typeof(Button))]
    public class UiButtonAudio : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private ButtonAudioPreset _preset;

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
                Debug.LogError($"ButtonAudioPreset is null: {name}", this);
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

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (_button.IsInteractable())
                _sharedAudioSource.PlayOneShot(_preset.HoverAudio);
        }
    }
}