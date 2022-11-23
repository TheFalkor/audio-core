using UnityEngine;
using AudioCoreLib.Structures;

namespace AudioCoreLib.Extensions
{
    [System.Serializable]
    public class SoundEffect
    {
        [Tooltip("Sets the name that will be used to access this audio clip when calling AudioCore.\nExample: BULLET_SHOT, PLAYER_JUMP..")]
        public string soundEffectName;

        [Space]

        [Tooltip("Audio clip.")]
        public AudioPackage audio;

        [Space]

        [Tooltip("Sets a factor of the sound volume.")]
        [Range(0f, 1f)]
        public float volume;

        [Tooltip("Sets the pitch of the sound effect to make it play slower or faster.")]
        [Range(-3f, 3f)]
        public float pitch;

        public void Reset()
        {
            volume = 1;
            pitch = 1;
        }
    }


    [CreateAssetMenu(fileName = "SFX Database", menuName = "AudioCore Databases/SFX Database", order = 1)]
    public class SFXDatabaseSO : ScriptableObject
    {
        public SoundEffect[] soundEffectDatabase;
        [HideInInspector] private int databaseLength = 0;

        public SoundEffect GetSoundEffect(string soundEffectName)
        {
            foreach (SoundEffect se in soundEffectDatabase)
            {
                if (se.soundEffectName == soundEffectName)
                {
                    return se;
                }
            }

            Debug.LogWarning("AudioCore: " + soundEffectName + " could not be found in linked sfx database.");
            return null;
        }

        private void OnValidate()
        {
            while (soundEffectDatabase.Length >  databaseLength)
            {
                soundEffectDatabase[databaseLength].Reset();
                databaseLength++;
            }

            databaseLength = soundEffectDatabase.Length;
        }
    }
}