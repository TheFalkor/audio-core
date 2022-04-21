using System.Collections;
using UnityEngine;
using AudioKorLib.Enums;
using AudioKorLib.Extensions;

namespace AudioKorLib.Structures
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
            source.volume = music.volume;
            source.pitch = music.pitch;
            source.loop = music.loop;
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
