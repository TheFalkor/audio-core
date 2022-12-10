using System.Collections.Generic;
using UnityEngine;
using AudioCoreLib.Enums;

namespace AudioCoreLib.Structures
{
    [System.Serializable]
    public class AudioPackage
    {
        public DatabaseEntryType audioType;
        public AudioClip audioClip;
        public List<AudioClip> audioClips;

        private int index = 0;


        public AudioClip GetAudioClip()
        {
            switch (audioType)
            {
                case DatabaseEntryType.Single_Clip:
                    return audioClip;

                case DatabaseEntryType.Cycle_Clips:
                    return audioClips[(index++) % audioClips.Count];

                case DatabaseEntryType.Randomize_Clip:
                    return audioClips[Random.Range(0, audioClips.Count)];
            }

            return null;
        }
    }
}
