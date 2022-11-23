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
                case DatabaseEntryType.SINGLE:
                    return audioClip;

                case DatabaseEntryType.CYCLE:
                    return audioClips[(index++) % audioClips.Count];

                case DatabaseEntryType.RANDOMIZE:
                    return audioClips[Random.Range(0, audioClips.Count)];
            }

            return null;
        }
    }
}
