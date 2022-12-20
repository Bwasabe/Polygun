using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SoundSlider : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup _group;

    private AudioMixer _audioMixer;


    private Slider _slider;

    
    private void Start() {
        _slider = GetComponent<Slider>();
        _audioMixer = SoundManager.Instance.AudioMixer;
    }

    public void ChangeVolume(float volume){
         if (volume == -40f) _audioMixer.SetFloat(_group.ToString(), -80);
        else _audioMixer.SetFloat(_group.ToString(), volume);
    }
}
