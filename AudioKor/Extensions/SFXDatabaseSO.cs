using UnityEngine;

namespace AudioKor.Extensions
{
    /// <summary>
    /// Sound effect data class.
    /// </summary>
    [System.Serializable]
    public class SoundEffect
    {
        /// <summary>
        /// Reference name of audio clip.
        /// </summary>
        [Tooltip("Reference name of audio clip. \nExample: BULLET_SHOT, PLAYER_JUMP..")]
        public string soundEffectName;

        /// <summary>
        /// Audio clip file.
        /// </summary>
        [Tooltip("Audio clip. Yep.")]
        public AudioClip audioClip;
    }

    /// <summary>
    /// Scriptable Object for creating SFX Databases.
    /// </summary>
    [CreateAssetMenu(fileName = "SFX Database", menuName = "AudioKor Databases/SFX Database", order = 1)]
    public class SFXDatabaseSO : ScriptableObject
    {
        /// <summary>
        /// Array of sound effects with a string as reference.
        /// </summary>
        public SoundEffect[] soundEffectDatabase;
    }
}