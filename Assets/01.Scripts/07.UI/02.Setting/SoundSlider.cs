using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SoundSlider : MonoBehaviour
{
    [SerializeField]
    private List<AudioMixerGroup> _group;

    private AudioMixer _audioMixer;


    private Slider _slider;

    private void Awake() {
        _slider = GetComponent<Slider>();
        _audioMixer = SoundManager.Instance.AudioMixer;
    }
    
    private void Start() {
        _slider.onValueChanged.AddListener(ChangeVolume);
    }

    private void OnEnable() {
        float value;
        if(_audioMixer.GetFloat(_group.ToString(), out value))
            _slider.value = value;
    }

    public void ChangeVolume(float volume){
        _group.ForEach(g =>
        {
            if (volume == -40f) _audioMixer.SetFloat(g.ToString(), -80f);
            else _audioMixer.SetFloat(g.ToString(), volume);
        });
    }
}
