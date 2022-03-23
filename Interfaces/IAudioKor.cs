using System.Collections;
using UnityEngine;

namespace AudioKor.Interfaces
{
    public interface IAudioKor
    {
        /// <summary>
        /// Pauses all audio components.
        /// </summary>
        public void Pause();

        /// <summary>
        /// Unpauses all audio components from a paused state. Only use in combination with Pause().
        /// </summary>
        public void UnPause();

        /// <summary>
        /// Stops all audio components.
        /// </summary>
        public void Stop();

        /// <summary>
        /// Plays an AudioClip, and scales the volume by SFXVolumeScale.
        /// </summary>
        /// <param name="clip">The audio clip to be played.</param>
        public void PlaySFX(AudioClip clip);

        /// <summary>
        /// Plays an AudioClip, and sets the volume to given volume value.
        /// </summary>
        /// <param name="clip">The audio clip to be played.</param>
        /// <param name="volume">Override volume used to play audio clip.</param>
        public void PlaySFX(AudioClip clip, float volume);

        // PlaySFX(clip, position)
        // PlaySFX(clip, delay)
        // PlaySFX(clip, loopcount)

    }
}

