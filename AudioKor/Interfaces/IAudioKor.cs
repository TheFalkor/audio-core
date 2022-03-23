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
        public void PlaySFXClip(AudioClip clip);

        /// <summary>
        /// Plays an AudioClip, and scales the volume by the volume value.
        /// </summary>
        /// <param name="clip">The audio clip to be played.</param>
        /// <param name="volume">The value to scale the audio clip volume.</param>
        public void PlaySFXClip(AudioClip clip, float volume = 1.0f);

        /// <summary>
        /// Plays an AudioClip from position.
        /// </summary>
        /// <param name="clip">The audio clip to be played.</param>
        /// <param name="position">The position that the audio clip will be played from.</param>
        public void PlaySFXClip(AudioClip clip, Vector3 position);

        /// <summary>
        /// Plays an AudioClip with a set delay.
        /// </summary>
        /// <param name="clip">The audio clip to be played.</param>
        /// <param name="delay">The delay in seconds until audio clip is played.</param>
        public void PlaySFXClipDelayed(AudioClip clip, float delay);

        public void PlaySFX(string soundEffectName);
        public void PlaySFX(string soundEffectName, float volume);
        public void PlaySFX(string soundEffectName, Vector3 position);
        public void PlaySFXDelayed(string soundEffectName, float delay);

        public void PlayMusicClip(AudioClip music);
        public void PlayMusicClip(AudioClip music, float volume);

        public void PlayMusic(string musicName);
        public void PlayMusic(string musicName, float volume);
        public void PlayMusic(string musicName, Vector3 position);
    }
}

