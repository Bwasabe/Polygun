using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[DontDestroyOnLoadAttribute]
public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField]
    private AudioMixer _audioMixer;
    public AudioMixer AudioMixer => _audioMixer;

    [Header("BGM For each Scene")]
    [SerializeField]
    private List<AudioClip> _bgms;

    private Dictionary<string, AudioClip> _audioDict = new Dictionary<string, AudioClip>();

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        // for (int i = 0; i < bglist.Length; i++)
        // {
        //     if (arg0.name == bglist[i].name) BgSoundPlay(bglist[i]);
        // }
    }
}
