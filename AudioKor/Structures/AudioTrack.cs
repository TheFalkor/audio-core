using System.Collections;
using UnityEngine;
using AudioKorLib.Structures;
using AudioKorLib.Extensions;

namespace AudioKorLib.Structures
{
    public class AudioTrack
    {
        public Music music;
        public AudioSource source;
        public float volume;


        public AudioTrack(AudioSource source)
        {
            this.source = source;
        }

        public void Tick(float deltaTime)
        {
            
        }

        public void Play(Music music)
        {
            this.music = music;
            source.volume = music.volume * volume;
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
            if (music != null)
                source.UnPause();
        }

        public void SetVolume(float volume)
        {
            this.volume = volume;
            if (music != null)
                source.volume = music.volume * volume;
            else
                source.volume = volume;
        }

        public bool IsAvailable(Music music)
        {
            return this.music == music || !source.isPlaying;
        }
    }
}
