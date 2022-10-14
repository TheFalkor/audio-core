using System;
using System.Collections.Generic;
using System.Text;

namespace AudioCoreLib.Structures
{
    public class Settings
    {
        public float masterVolume;
        public float musicVolume;
        public float sfxVolume;

        public Settings(float masterVolume, float musicVolume, float sfxVolume)
        {
            this.masterVolume = masterVolume;
            this.musicVolume = musicVolume;
            this.sfxVolume = sfxVolume;
        }
    }
}
