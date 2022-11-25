using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioCoreLib.Extensions;
using AudioCoreLib.Interfaces;
using AudioCoreLib.Structures;


/// <summary>
/// AudioCore is an Audio Manager made by Henrik Nilsson.<br></br>https://www.github.com/TheFalkor
/// </summary>
public sealed class AudioCore : MonoBehaviour, IAudioCore
{
    [Header("Database References")]
    public MusicDatabaseSO musicDatabase;
    public SFXDatabaseSO sfxDatabase;
    
    [Header("Game Settings")]
    [Tooltip("This music from the database will be played when the game starts.")]
    [SerializeField] private string playOnAwake = "";

    [Header("Audio Settings")]
    [Tooltip("Sets the overall volume of all audio components")]
    [Range(0f, 1f)]
    public float masterVolume = 1;
    [HideInInspector] private float currentMasterVolume;

    [Tooltip("Sets the overall volume of all music.")]
    [Range(0f, 1f)]
    public float musicVolume = 1;
    [HideInInspector] private float currentMusicVolume;

    [Tooltip("Sets the overall volume of all sound effects.")]
    [Range(0f, 1f)]
    public float SFXVolume = 1;
    [HideInInspector] private float currentSFXVolume;

    [Header("Application Settings")]
    [Tooltip("Sets the amount of available music tracks to exist.")]
    [Range(0, 8)]
    public int musicTrackCount = 1;

    [Space]

    [Tooltip("Prevents AudioCore from being destroyed when loading scenes.")]
    public bool dontDestroyOnLoad = false;

    [Tooltip("Makes sure that this is the only instance of AudioCore in the scene.\nWill destroy all other instances.")]
    public bool forceUniqueInstance = false;

    [HideInInspector] private AudioTrack[] audioTracks;
    [HideInInspector] private AudioSource sfxSource;

    [HideInInspector] private bool initialized = false;
    [HideInInspector] private static AudioCore instance;


    private void Awake()
    {
        if (instance && instance.forceUniqueInstance)
        {
            Destroy(this);
            return;
        }

        if (dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
        
        instance = this;

        currentMasterVolume = masterVolume;
        currentMusicVolume = musicVolume;
        currentSFXVolume = SFXVolume;

        audioTracks = new AudioTrack[musicTrackCount];
        if (musicTrackCount > 0)
        {
            GameObject trackParent = new GameObject("AudioCore Tracks");
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

        if (playOnAwake != "")
            PlayMusic(playOnAwake);
    }

    private void Update()
    {
        foreach (AudioTrack at in audioTracks)
            at.Tick(Time.deltaTime);
    }

    public void SetMusic(string musicName, AudioCore.Track track)
    {
        if (!initialized)
        {
            Debug.LogWarning("AudioCore: SetMusic() was called before AudioCore finished preparing.");
            return;
        }

        if (musicDatabase == null)
        {
            Debug.LogWarning("AudioCore: No Music Database attached to Audio Core.");
            return;
        }

        if ((int)track >= musicTrackCount)
        {
            Debug.LogWarning("AudioCore: Track " + track.ToString() + " is out of range, increase musicTrackCount in the settings");
            return;
        }

        Music music = musicDatabase.GetMusic(musicName);

        if (music == null)
            return;

        audioTracks[(int)track].SetMusic(music);
    }

    public void PlayMusic(string musicName)
    {
        if (musicDatabase == null)
        {
            Debug.LogWarning("AudioCore: No Music Database attached to Audio Core.");
            return;
        }

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

        Debug.LogWarning("AudioCore: No available Audio Track could be found. Current Audio Tracks: " + audioTracks.Length);
    }

    public void PlayMusic(string musicName, AudioCore.Track track)
    {
        if (!initialized)
        {
            Debug.LogWarning("AudioCore: PlayMusic() was called before AudioCore finished preparing.");
            return;
        }

        if (musicDatabase == null)
        {
            Debug.LogWarning("AudioCore: No Music Database attached to Audio Core.");
            return;
        }

        if ((int)track >= musicTrackCount)
        {
            Debug.LogWarning("AudioCore: Track " + track.ToString() + " is out of range, increase musicTrackCount in the settings");
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

    public void PauseMusic(AudioCore.Track track)
    {
        if ((int)track >= musicTrackCount)
        {
            Debug.LogWarning("AudioCore: Track " + track.ToString() + " is out of range, increase musicTrackCount in the settings");
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

    public void ResumeMusic(AudioCore.Track track)
    {
        if ((int)track >= musicTrackCount)
        {
            Debug.LogWarning("AudioCore: Track " + track.ToString() + " is out of range, increase musicTrackCount in the settings");
            return;
        }

        audioTracks[(int)track].ResumeTrack();
    }

    public void FadeInMusic(AudioCore.Track track, float duration)
    {
        if ((int)track >= musicTrackCount)
        {
            Debug.LogWarning("AudioCore: Track " + track.ToString() + " is out of range, increase musicTrackCount in the settings");
            return;
        }

        audioTracks[(int)track].FadeIn(duration);
    }
    public void FadeInMusic(AudioCore.Track track, float duration, string musicName)
    {
        if (musicDatabase == null)
        {
            Debug.LogWarning("AudioCore: No Music Database attached to Audio Core.");
            return;
        }

        if ((int)track >= musicTrackCount)
        {
            Debug.LogWarning("AudioCore: Track " + track.ToString() + " is out of range, increase musicTrackCount in the settings");
            return;
        }

        Music music = musicDatabase.GetMusic(musicName);

        if (music == null)
            return;

        audioTracks[(int)track].FadeIn(duration, music);
    }
    public void FadeOutMusic(AudioCore.Track track, float duration)
    {
        if ((int)track >= musicTrackCount)
        {
            Debug.LogWarning("AudioCore: Track " + track.ToString() + " is out of range, increase musicTrackCount in the settings");
            return;
        }

        audioTracks[(int)track].FadeOut(duration);
    }

    public void FadeOutAll(float duration)
    {
        foreach (AudioTrack at in audioTracks)
            at.FadeOut(duration);
    }

    public void PlaySFX(string soundEffectName)
    {
        if (sfxDatabase == null)
        {
            Debug.LogWarning("AudioCore: No SFX Database attached to Audio Core.");
            return;
        }

        SoundEffect soundEffect = sfxDatabase.GetSoundEffect(soundEffectName);
        
        if (soundEffect == null)
            return;

        sfxSource.pitch = soundEffect.pitch;

        foreach (AudioModifier modifier in soundEffect.modifiers)
            modifier.TriggerModifier(sfxSource);

        sfxSource.PlayOneShot(soundEffect.audio.GetAudioClip(), soundEffect.volume * masterVolume * SFXVolume);
    }

    public void PlaySFX(string soundEffectName, float volumeScale)
    {
        if (sfxDatabase == null)
        {
            Debug.LogWarning("AudioCore: No SFX Database attached to Audio Core.");
            return;
        }

        if (volumeScale < 0)
        {
            Debug.LogWarning("AudioCore: Volume is out of range, attempting to scale volume by negative value.");
            return;
        }

        SoundEffect soundEffect = sfxDatabase.GetSoundEffect(soundEffectName);

        if (soundEffect == null)
            return;

        sfxSource.pitch = soundEffect.pitch;

        foreach (AudioModifier modifier in soundEffect.modifiers)
            modifier.TriggerModifier(sfxSource);

        sfxSource.PlayOneShot(soundEffect.audio.GetAudioClip(), soundEffect.volume * masterVolume * SFXVolume * volumeScale);
    }

    public void SetMasterVolume(float masterVolume)
    {
        this.masterVolume = masterVolume;
        currentMasterVolume = masterVolume;

        foreach (AudioTrack at in audioTracks)
            at.SetVolume(masterVolume * musicVolume);

        sfxSource.volume = masterVolume * SFXVolume;
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
        this.SFXVolume = sfxVolume;
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

        if (currentSFXVolume != SFXVolume)
            SetSFXVolume(SFXVolume);
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




