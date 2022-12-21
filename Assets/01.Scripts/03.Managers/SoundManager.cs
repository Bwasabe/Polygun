using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Reflection;

public enum AudioType
{
    BGM,
    SFX,
    Voice,
    IgnorePitch,

    Length
}

[DontDestroyOnLoadAttribute]
public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField]
    private AudioMixer _audioMixer;
    public AudioMixer AudioMixer => _audioMixer;

    [Header("BGM For each Scene")]
    [SerializeField]
    private List<AudioClip> _bgms; // BuildingScene 순서로 BGM 배치

    private Dictionary<string, AudioClip> _bgmDict = new Dictionary<string, AudioClip>();

    private Dictionary<AudioType, MethodInfo> _typeMethod = new Dictionary<AudioType, MethodInfo>();
    private AudioSource[] _audioSources = new AudioSource[(int)AudioType.Length];


    protected override void Awake()
    {
        base.Awake();
        Init();
        CallInitMethod();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Init()
    {
        for (int i = 0; i < (int)AudioType.Length; ++i)
        {
            GameObject g = new GameObject(((AudioType)i).ToString());
            g.transform.SetParent(transform);
            _audioSources[i] = g.AddComponent<AudioSource>();
            _audioSources[i].playOnAwake = false;
        }
        _audioSources[(int)AudioType.BGM].loop = true;
    }
    private void InitBGM()
    {
        for (int i = 0; i < _bgms.Count; ++i)
        {
            _bgmDict.Add(((BuildingScenes)i).ToString(), _bgms[i]);
        }
    }

    private void CallInitMethod()
    {
        Type type = GetType();

        for (int i = 0; i < (int)AudioType.Length; ++i)
        {
            MethodInfo info = type.GetMethod($"Play{((AudioType)i).ToString()}",
                BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            _typeMethod.Add((AudioType)i, info);
        }
    }

    private void PlayIgnorePitch(AudioClip clip)
    {
        if(clip == null)
        {
            Debug.Log("Clip is Null");
            return;
        }
        AudioSource audioSource = _audioSources[(int)AudioType.IgnorePitch];
        audioSource.PlayOneShot(clip);
    }

    private void PlayBGM(AudioClip clip)
    {
        if(clip == null)
        {
            Debug.Log("Clip is Null");
            return;
        }
        AudioSource audioSource = _audioSources[(int)AudioType.BGM];
        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.clip = clip;
        audioSource.Play();
    }

    private void PlayVoice(AudioClip clip)
    {
        if(clip == null)
        {
            Debug.Log("Clip is Null");
            return;
        }

        AudioSource audioSource = _audioSources[(int)AudioType.Voice];
        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.clip = clip;
        audioSource.Play();
    }

    private void PlaySFX(AudioClip clip)
    {
        if(clip == null)
        {
            Debug.Log("Clip is Null");
            return;
        }

        AudioSource audioSource = _audioSources[(int)AudioType.SFX];
        audioSource.PlayOneShot(clip);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        try
        {
            Play(AudioType.BGM,_bgmDict[scene.name]);
        }
        catch{}

        // for (int i = 0; i < (int)BuildingScenes.Length; i++)
        // {
        //     if (scene.name.Equals((BuildingScenes)i))
        //     {
        //         Play(AudioType.BGM, _bgms[i]);
        //     }
        // }
    }

    public void Play(AudioType type, AudioClip clip)
    {
        _typeMethod[type].Invoke(this, new object[] { clip });
    }

    public void SetPitch(AudioType type, float pitch)
    {
        _audioSources[(int)type].pitch = pitch;
    }

    public float GetPitch(AudioType type)
    {
        return _audioSources[(int)type].pitch;
    }

    public void SetAllSourcePitch(float pitch)
    {
        for (int i = 0; i < _audioSources.Length; ++i)
        {
            if (i == (int)AudioType.IgnorePitch) continue;

            _audioSources[i].pitch = pitch;
        }
    }

    // public void AddAudio(string key, AudioClip clip)
    // {
    //     if (_audioDict.TryGetValue(key, out AudioClip value))
    //     {
    //         Debug.LogError($"{key} is already exist by {value}");
    //         return;
    //     }
    //     else
    //     {
    //         _audioDict.Add(key, value);
    //     }
    // }
}
