using UnityEngine;
using AudioCoreLib.Enums;

namespace AudioCoreLib.Structures
{
    [System.Serializable]
    public class AudioModifier
    {
        public AudioModifierType modifierType;
        public float minimumValue;
        public float maximumValue;

        public void TriggerModifier(AudioSource sfxSource)
        {
            switch (modifierType)
            {
                case AudioModifierType.Randomize_Pitch:
                    if (minimumValue > maximumValue)
                    {
                        Debug.LogWarning("AudioCore: (Randomized Pitch): Maximum value is less than minimum value.");
                        return;
                    }
                    sfxSource.pitch = Random.Range(minimumValue, maximumValue);
                    break;

                case AudioModifierType.Randomize_Volume:
                    if (minimumValue > maximumValue)
                    {
                        Debug.LogWarning("AudioCore: (Randomized Volume): Maximum value is less than minimum value.");
                        return;
                    }
                    sfxSource.volume = Random.Range(minimumValue, maximumValue);
                    break;
            }
        }
    }
}
