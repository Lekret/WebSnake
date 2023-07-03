using UnityEngine;

namespace WebSnake.UI.Utils
{
    [CreateAssetMenu(menuName = "Config/" + nameof(ButtonAudioPreset), fileName = nameof(ButtonAudioPreset))]
    public class ButtonAudioPreset : ScriptableObject
    {
        public AudioClip HoverAudio;
        public AudioClip ClickAudio;
    }
}