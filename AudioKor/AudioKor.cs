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
    private float currentMasterVolume;

    [Tooltip("Sets the overall volume of all music.")]
    [Range(0f, 1f)]
    public float musicVolume;
    private float currentMusicVolume;

    [Tooltip("Sets the overall volume of all sound effects.")]
    [Range(0f, 1f)]
    public float sfxVolume;
    private float currentSFXVolume;

    [Header("Application Settings")]
    [Tooltip("Sets the amount of available music tracks to exist.")]
    [Range(0, 8)]
    public int musicTrackCount = 1;

    [Tooltip("Prevents AudioKor from being destroyed. Will destroyed duplicate AudioKors in new scenes.")]
    public bool dontDestroyOnLoad = false;

    private AudioTrack[] audioTracks;
    private AudioSource sfxSource;

    private bool initialized = false;
    private static AudioKor instance;


    private void Awake()
    {
        if (instance)
        {
            if (instance.dontDestroyOnLoad)
            {
                Destroy(this);
                return;
            }
        }

        instance = this;
    }

    private void Start()
    {
        currentMasterVolume = masterVolume;
        currentMusicVolume = musicVolume;
        currentSFXVolume = sfxVolume;

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

        initialized = true;
    }

    private void Update()
    {
        foreach (AudioTrack at in audioTracks)
            at.Tick(Time.deltaTime);
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

        Debug.LogWarning("AudioKor: No available Audio Track could be found. Current Audio Tracks: " + musicTrackCount);
    }

    public void PlayMusic(string musicName, AudioKor.Track track)
    {
        if ((int)track >= musicTrackCount)
        {
            Debug.LogWarning("AudioKor: Track " + track.ToString() + " is out of range, increase musicTrackCount in the settings");
            return;
        }

        Music music = musicDatabase.GetMusic(musicName);

        if (music == null)
            return;

        audioTracks[(int)track].Play(music);
    }

    public void PauseMusic()
    {
        foreach (AudioTrack at in audioTracks)
        {
            at.PauseTrack();
        }
    }

    public void PauseMusic(AudioKor.Track track)
    {
        if ((int)track >= musicTrackCount)
        {
            Debug.LogWarning("AudioKor: Track " + track.ToString() + " is out of range, increase musicTrackCount in the settings");
            return;
        }

        audioTracks[(int)track].PauseTrack();
    }

    public void ResumeMusic()
    {
        foreach (AudioTrack at in audioTracks)
        {
            at.ResumeTrack();
        }
    }

    public void ResumeMusic(AudioKor.Track track)
    {
        if ((int)track >= musicTrackCount)
        {
            Debug.LogWarning("AudioKor: Track " + track.ToString() + " is out of range, increase musicTrackCount in the settings");
            return;
        }

        audioTracks[(int)track].ResumeTrack();
    }

    public void PlaySFX(string soundEffectName)
    {
        SoundEffect soundEffect = sfxDatabase.GetSoundEffect(soundEffectName);

        if (soundEffect == null)
            return;

        sfxSource.pitch = soundEffect.pitch;
        sfxSource.PlayOneShot(soundEffect.audioClip, soundEffect.volume * masterVolume * sfxVolume);
    }

    public void PlaySFX(string soundEffectName, float volumeScale)
    {
        if (volumeScale < 0)
        {
            Debug.LogWarning("AudioKor: Volume is out of range, attempting to scale volume by negative value.");
            return;
        }

        SoundEffect soundEffect = sfxDatabase.GetSoundEffect(soundEffectName);

        if (soundEffect == null)
            return;

        sfxSource.pitch = soundEffect.pitch;
        sfxSource.PlayOneShot(soundEffect.audioClip, soundEffect.volume * masterVolume * sfxVolume * volumeScale);
    }

    public void SetMasterVolume(float masterVolume)
    {
        this.masterVolume = masterVolume;
        currentMasterVolume = masterVolume;

        foreach (AudioTrack at in audioTracks)
            at.SetVolume(masterVolume * musicVolume);

        sfxSource.volume = masterVolume * sfxVolume;
    }

    public void SetMusicVolume(float musicVolume)
    {
        this.musicVolume = musicVolume;
        currentMusicVolume = musicVolume;

        foreach (AudioTrack at in audioTracks)
            at.SetVolume(masterVolume * musicVolume);
    }

    public void SetSFXVolume(float sfxVolume)
    {
        this.sfxVolume = sfxVolume;
        currentSFXVolume = sfxVolume;
    }
    
    private void OnValidate()
    {
        if (!Application.isPlaying || !initialized)
            return;

        if (currentMasterVolume != masterVolume)
            SetMasterVolume(masterVolume);

        if (currentMusicVolume != musicVolume)
            SetMusicVolume(musicVolume);

        if (currentSFXVolume != sfxVolume)
            SetSFXVolume(sfxVolume);
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




