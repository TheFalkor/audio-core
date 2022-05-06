using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioKorLib.Extensions;
using AudioKorLib.Interfaces;
using AudioKorLib.Structures;


public sealed class AudioKor : MonoBehaviour, IAudioKor
{
    [Header("Database References")]
    public MusicDatabaseSO musicDatabase;
    public SFXDatabaseSO sfxDatabase;

    [Header("Audio Settings")]
    [Tooltip("Sets the overall volume of all audio components")]
    [Range(0f, 1f)]
    public float masterVolume;

    [Tooltip("Sets the overall volume of all music.")]
    [Range(0f, 1f)]
    public float musicVolume;

    [Tooltip("Sets the overall volume of all sound effects.")]
    [Range(0f, 1f)]
    public float sfxVolume;

    [Header("Product Settings")]
    [Tooltip("Sets the amount of available music tracks to exist.")]
    [Range(0, 8)]
    public int musicTrackCount;

    private AudioTrack[] audioTracks;
    private AudioSource sfxSource;


    private void Start()
    {
        audioTracks = new AudioTrack[musicTrackCount];
        if (musicTrackCount > 0)
        {
            GameObject trackParent = new GameObject("AudioKor Tracks");
            trackParent.transform.parent = transform;

            for (int i = 0; i < audioTracks.Length; i++)
            {
                AudioSource source = trackParent.AddComponent<AudioSource>();
                audioTracks[i] = new AudioTrack(source);

                audioTracks[i].SetVolume(masterVolume * musicVolume);
            }
        }

        sfxSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {

    }

    public void PlayMusic(string musicName)
    {
        Music music = musicDatabase.GetMusic(musicName);

        if (music == null)
            return;

        foreach (AudioTrack at in audioTracks)
        {
            if (at.IsAvailable(music))
            {
                at.Play(music);
                return;
            }
        }
    }

    public void PlayMusic(string musicName, AudioKor.Track track)
    {
        if ((int)track >= musicTrackCount)
        {
            Debug.LogWarning("AudioKor: Track " + track.ToString() + " is out of range, increase the musicTrackCount");
            return;
        }

        Music music = musicDatabase.GetMusic(musicName);

        if (music == null)
            return;

        audioTracks[(int)track].Play(music);
    }

    public void PauseMusic()
    {
        foreach (AudioTrack track in audioTracks)
        {
            track.PauseTrack();
        }
    }

    public void PauseMusic(AudioKor.Track track)
    {
        audioTracks[(int)track].PauseTrack();
    }

    public void PlaySFX(string soundEffectName)
    {
        SoundEffect soundEffect = sfxDatabase.GetSoundEffect(soundEffectName);

        if (soundEffect == null)
            return;

        sfxSource.pitch = soundEffect.pitch;
        sfxSource.PlayOneShot(soundEffect.audioClip, soundEffect.volume * masterVolume * sfxVolume);
    }

    public void SetMasterVolume(float masterVolume)
    {
        this.masterVolume = masterVolume;

        foreach (AudioTrack track in audioTracks)
            track.SetVolume(masterVolume * musicVolume);

        sfxSource.volume = masterVolume * sfxVolume;
    }

    public void SetMusicVolume(float musicVolume)
    {
        this.musicVolume = musicVolume;

        foreach (AudioTrack track in audioTracks)
            track.SetVolume(masterVolume * musicVolume);
    }

    public void SetSFXVolume(float sfxVolume)
    {
        this.sfxVolume = sfxVolume;
    }

    /// <summary>
    /// Used to specify which Audio Track to affect.
    /// </summary>
    public enum Track
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H
    }
}




