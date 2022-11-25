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
                case AudioModifierType.RANDOMIZE_PITCH:
                    if (minimumValue > maximumValue)
                    {
                        Debug.LogWarning("AudioCore: (Randomized_Pitch): Maximum value is less than minimum value.");
                        return;
                    }
                    sfxSource.pitch = Random.Range(minimumValue, maximumValue);
                    break;
            }
        }
    }
}
