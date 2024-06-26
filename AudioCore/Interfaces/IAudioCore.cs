using System.Collections;
using UnityEngine;
using AudioCoreLib.Structures;

namespace AudioCoreLib.Interfaces
{
    public interface IAudioCore
    {
        /// <summary>
        /// Sets the music data into the track without starting it.
        /// </summary>
        /// <param name="musicName">The music asset to be played.</param>
        /// <param name="track">The track that will be used.</param>
        public void SetMusic(string musicName, AudioCore.Track track);

        /// <summary>
        /// Starts a track that plays music.
        /// </summary>
        /// <param name="musicName">The music asset to be played.</param>
        public void PlayMusic(string musicName);

        /// <summary>
        /// Starts a track that plays music.
        /// </summary>
        /// <param name="musicName">The music asset to be played.</param>
        /// <param name="track">The track that will be used.</param>
        public void PlayMusic(string musicName, AudioCore.Track track);

        /// <summary>
        /// Pauses music and ambience playing from all tracks.
        /// </summary>
        public void PauseMusic();

        /// <summary>
        /// Pauses music and ambience playing from one track.
        /// </summary>
        /// <param name="track">The track that will be paused.</param>
        public void PauseMusic(AudioCore.Track track);

        /// <summary>
        /// Resume all tracks.
        /// </summary>
        public void ResumeMusic();

        /// <summary>
        /// Resumes one track.
        /// </summary>
        /// <param name="track">The track that will be resumed.</param>
        public void ResumeMusic(AudioCore.Track track);

        /// <summary>
        /// Fade in the track.
        /// </summary>
        /// <param name="track">The track that will be faded.</param>
        /// <param name="duration">The duration of the fade.</param>
        public void FadeInMusic(AudioCore.Track track, float duration);

        /// <summary>
        /// Set the music asset and fade in the track.
        /// </summary>
        /// <param name="track">The track that will be faded.</param>
        /// <param name="duration">The duration of the fade.</param>
        /// <param name="musicName">The music asset to be played.</param>
        public void FadeInMusic(AudioCore.Track track, float duration, string musicName);

        /// <summary>
        /// Fade out the track.
        /// </summary>
        /// <param name="track">The track that will be faded.</param>
        /// <param name="duration">The duration of the fade.</param>
        /// <param name="stopOnComplete">Stops the audio player on fade completion.</param>
        public void FadeOutMusic(AudioCore.Track track, float duration, bool stopOnComplete);

        /// <summary>
        /// Fade out all available tracks.
        /// </summary>
        /// <param name="duration">The duration of the fade.</param>
        /// <param name="stopOnComplete">Stops the audio player on fade completion.</param>
        public void FadeOutAll(float duration, bool stopOnComplete);

        /// <summary>
        /// Plays a sound effect.
        /// </summary>
        /// <param name="soundEffectName">The sound effect name to be played.</param>
        public void PlaySFX(string soundEffectName);

        /// <summary>
        /// Plays a sound effect with additional volume scale.
        /// </summary>
        /// <param name="soundEffectName">The sound effect name to be played.</param>
        /// <param name="volumeScale">The volume to scale the SFX by.</param>
        public void PlaySFX(string soundEffectName, float volumeScale);

        /// <summary>
        /// Plays a directional sound effect from provided position.
        /// </summary>
        /// <param name="soundEffectName">The sound effect name to be played.</param>
        /// <param name="position">The position of the sound origin.</param>
        public void PlaySFX(string soundEffectName, Vector3 position);

        /// <summary>
        /// Plays a directional sound effect from provided position with additional volume scale.
        /// </summary>
        /// <param name="soundEffectName">The sound effect name to be played.</param>
        /// <param name="position">The position of the sound origin.</param>
        /// <param name="volumeScale">The volume to scale the SFX by.</param>
        public void PlaySFX(string soundEffectName, Vector3 position, float volumeScale);

        /// <summary>
        /// Sets the general volume of all audio components.
        /// </summary>
        /// <param name="masterVolume">The general volume for all components.</param>
        public void SetMasterVolume(float masterVolume);

        /// <summary>
        /// Sets the volume of all music tracks.
        /// </summary>
        /// <param name="musicVolume">The volume for all music tracks.</param>
        public void SetMusicVolume(float musicVolume);

        /// <summary>
        /// Sets the volume for all sound effects.
        /// </summary>
        /// <param name="sfxVolume">The volume for all sound effects.</param>
        public void SetSFXVolume(float sfxVolume);
    }
}

