using UnityEngine;

namespace WebSnake.UI.Utils
{
    [CreateAssetMenu(menuName = "Config/" + nameof(UiAudioPreset), fileName = nameof(UiAudioPreset))]
    public class UiAudioPreset : ScriptableObject
    {
        public AudioClip ClickAudio;
    }
}