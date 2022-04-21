using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioKorLib.Enums;
using AudioKorLib.Extensions;
using AudioKorLib.Interfaces;
using AudioKorLib.Structures;


public sealed class AudioKor : MonoBehaviour, IAudioKor
{
    [Header("Database References")]
    public MusicDatabaseSO musicDatabase;
    public SFXDatabaseSO sfxDatabase;

    private readonly AudioTrack[] audioTracks = new AudioTrack[8];

    private AudioSource sfxSource;


    private void Start()
    {
        for (int i = 0; i < audioTracks.Length; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            audioTracks[i] = new AudioTrack(source);
        }

        sfxSource = gameObject.AddComponent<AudioSource>();
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
        foreach (AudioTrack track in audioTracks)
        {
            track.PauseTrack();
        }
    }

    public void PauseMusic(Track track)
    {
        audioTracks[(int)track].PauseTrack();
    }

    public void PlaySFX(string soundEffectName)
    {
        SoundEffect soundEffect = sfxDatabase.GetSoundEffect(soundEffectName);

        sfxSource.PlayOneShot(soundEffect.audioClip);
    }

}




