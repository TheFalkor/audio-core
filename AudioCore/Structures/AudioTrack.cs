using UnityEngine;
using AudioCoreLib.Extensions;
using AudioCoreLib.Enums;

namespace AudioCoreLib.Structures
{
    public class AudioTrack
    {
        private Music music;
        private readonly AudioSource source;
        private float volume;

        private AudioTrackState state = AudioTrackState.Resting;
        private float fadeDuration = 0;
        private float currentFadeDuration = 0;
        private float targetFadeVolume = 0;
        private float startFadeVolume = 0;


        public AudioTrack(AudioSource source)
        {
            this.source = source;
        }

        public void Tick(float deltaTime)
        {
            if (state != AudioTrackState.Fading)
                return;

            currentFadeDuration += deltaTime;

            if (currentFadeDuration >= fadeDuration)
            {
                currentFadeDuration = fadeDuration;

                if (targetFadeVolume == 0)
                {
                    state = AudioTrackState.Resting;
                    source.Stop();
                }
                else
                    state = AudioTrackState.Playing;
            }

            float fadedVolume = startFadeVolume + (targetFadeVolume - startFadeVolume) * (currentFadeDuration / fadeDuration);
            source.volume = fadedVolume;
        }

        public void Play(Music music)
        {
            SetMusic(music);
            source.Play();
            state = AudioTrackState.Playing;
        }

        public void PauseTrack()
        {
            if (source.isPlaying)
                state = AudioTrackState.Paused;

            source.Pause();
        }

        public void ResumeTrack()
        {
            if (music == null)
                return;

            source.UnPause();
            state = AudioTrackState.Playing;
        }

        public void FadeIn(float duration, Music music = null)
        {
            if (duration < 0)
            {
                Debug.LogWarning("AudioCore: Fade duration cannot be less than zero.");
                return;
            }

            if (!source.isPlaying)
                source.volume = 0;

            if (music != null)
                SetMusic(music, true);
            else if (this.music == null)
            {
                Debug.LogWarning("AudioCore: Attempting to fade in with no Music File selected.");
                return;
            }

            if (!source.isPlaying)
                source.Play();

            state = AudioTrackState.Fading;
            fadeDuration = duration;
            currentFadeDuration = 0;

            startFadeVolume = source.volume;
            targetFadeVolume = this.music.volume * volume;
        }

        public void FadeOut(float duration)
        {
            if (duration < 0)
            {
                Debug.LogWarning("AudioCore: Fade duration cannot be less than zero");
                return;
            }

            if (!source.isPlaying)
                return;

            state = AudioTrackState.Fading;
            fadeDuration = duration;
            currentFadeDuration = 0;

            startFadeVolume = source.volume;
            targetFadeVolume = 0;
        }

        public void SetVolume(float volume)
        {
            this.volume = volume;
            float sourceVolume = music != null ? music.volume * volume : volume;

            if (state != AudioTrackState.Fading)
                source.volume = sourceVolume;
            else if (targetFadeVolume > 0)
                targetFadeVolume = sourceVolume;
        }

        public bool IsAvailable(Music music)
        {
            return this.music == music || !source.isPlaying;
        }

        public void SetMusic(Music music, bool ignoreVolume = false)
        {
            this.music = music;
            source.pitch = music.pitch;
            source.loop = music.loop;
            source.clip = music.audioClip;

            if (!ignoreVolume)
                source.volume = music.volume * volume;
        }
    }
}
