using UnityEngine;

namespace AudioKor.Extensions
{
    [System.Serializable]
    public class SoundEffect
    {
        [Tooltip("Reference name of audio clip. \nExample: BULLET_SHOT, PLAYER_JUMP..")]
        public string soundEffectName;

        [Tooltip("Audio clip. Yep.")]
        public AudioClip audioClip;
    }


    [CreateAssetMenu(fileName = "SFX Database", menuName = "AudioKor Databases/SFX Database", order = 1)]
    public class SFXListScriptableObject : ScriptableObject
    {
        public SoundEffect[] soundEffectDatabase;
    }
}