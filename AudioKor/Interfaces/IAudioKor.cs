using System.Collections;
using UnityEngine;
using AudioKorLib.Enums;
using AudioKorLib.Structures;

namespace AudioKorLib.Interfaces
{
    public interface IAudioKor
    {
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
        public void PlayMusic(string musicName, Track track);

        /// <summary>
        /// Pauses music and ambience playing from all tracks.
        /// </summary>
        public void PauseMusic();

        /// <summary>
        /// Pauses music and ambience playing from one track.
        /// </summary>
        /// <param name="track">The track that will be paused.</param>
        public void PauseMusic(Track track);

        public void PlaySFX(string soundEffectName);
    }
}

