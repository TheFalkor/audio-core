using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioKor.Enums;
using AudioKor.Extensions;
using AudioKor.Interfaces;
using AudioKor.Structures;

namespace AudioKor
{
    public sealed class AudioKor : MonoBehaviour, IAudioKor
    {
        [Header("Database References")]
        public MusicDatabaseSO musicDatabase;
        public SFXDatabaseSO sfxDatabase;

        private readonly AudioTrack[] audioTracks = new AudioTrack[8];


        private void Start()
        {
            for (int i = 0; i < audioTracks.Length; i++)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                audioTracks[i] = new AudioTrack(source);
            }

        }


        private void Update()
        {

        }

        public void PlayMusic(string musicName)
        {
            Music music = musicDatabase.GetMusic(musicName);

            foreach (AudioTrack at in audioTracks)
            {
                if (at.IsAvailable(music))
                {
                    at.Play(music);
                    return;
                }
            }
        }

        public void PlayMusic(string musicName, Track track)
        {
            Music music = musicDatabase.GetMusic(musicName);

            audioTracks[(int)track].Play(music);
        }

        public void PauseMusic()
        {
            throw new System.NotImplementedException();
        }

        public void PauseMusic(Track track)
        {
            throw new System.NotImplementedException();
        }

        public void PlaySFX(string soundEffectName)
        {
            throw new System.NotImplementedException();
        }

    }
}



