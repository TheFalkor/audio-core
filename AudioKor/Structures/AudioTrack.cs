using System.Collections;
using UnityEngine;
using AudioKor.Enums;
using AudioKor.Extensions;

namespace AudioKor.Structures
{
    public class AudioTrack
    {
        public Track track;
        public Music music;
        public AudioSource source;


        public AudioTrack(AudioSource source)
        {
            this.source = source;
        }

        public void Play(Music music)
        {
            this.music = music;
            source.clip = music.audioClip;
            source.Play();
        }

        public void PauseTrack()
        {
            source.Pause();
        }

        public void ResumeTrack()
        {
            source.UnPause();
        }

        public void SetSettings(/*Settings Struct*/)
        {

        }

        public bool IsAvailable(Music music)
        {
            return this.music == music || !source.isPlaying;
        }
    }
}
