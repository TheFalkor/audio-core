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

    [Header("Settings")]
    [Tooltip("Sets the overall volume of all audio components")]
    [Range(0f, 1f)]
    public float masterVolume;

    [Tooltip("Sets the overall volume of all music.")]
    [Range(0f, 1f)]
    public float musicVolume;

    [Tooltip("Sets the overall volume of all sound effects.")]
    [Range(0f, 1f)]
    public float sfxVolume;

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

        sfxSource.volume = soundEffect.volume;
        sfxSource.pitch = soundEffect.pitch;
        sfxSource.PlayOneShot(soundEffect.audioClip);
    }
}




