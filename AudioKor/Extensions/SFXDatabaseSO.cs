using UnityEngine;

namespace AudioKor.Extensions
{
    [System.Serializable]
    public class SoundEffect
    {
        [Tooltip("Sets the name that will be used to access this audio clip when calling AudioKor.\nExample: BULLET_SHOT, PLAYER_JUMP..")]
        public string soundEffectName;

        [Tooltip("Audio clip. Yep.")]
        public AudioClip audioClip;

        [Space]

        [Tooltip("Sets the audio clip to loop once it stops playing.")]
        public bool loop;

        [Space]

        [Tooltip("Sets a factor of the sound volume.")]
        [Range(0f, 1f)]
        public float volume = 1f;

        [Tooltip("Sets the pitch of the sound effect to make it play slower or faster.")]
        [Range(-3f, 3f)]
        public float pitch = 1f;
    }


    [CreateAssetMenu(fileName = "SFX Database", menuName = "AudioKor Databases/SFX Database", order = 1)]
    public class SFXDatabaseSO : ScriptableObject
    {
        public SoundEffect[] soundEffectDatabase;
    }
}