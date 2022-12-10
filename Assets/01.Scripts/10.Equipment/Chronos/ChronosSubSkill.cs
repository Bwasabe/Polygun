using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using DG.Tweening;

public class ChronosSubSkill : BaseSkill, ISkillInitAble, ISkillPersistAble
{
    private PlayerStat _playerStat;
    private ChronosData _data;

    private bool _isUsed;

    private const string Q = "Q";

    private InputManager _input;
    private LiftGammaGain _liftGammaGain;

    private Tweener _gammaDownTweener;
    private Tweener _gammaUpTweener;
    public ChronosSubSkill(BaseEquipment parent) : base(parent)
    {
        _playerStat = GameManager.Instance.Player.PlayerStat;
        _input = GameManager.Instance.InputManager;
        _data = _parent.GetData<ChronosData>();

        // _data.TimeStopSlider.maxValue = _data.TimeStopCoolTime;

        if (!_data.GlobalVolume.profile.TryGet(out _liftGammaGain)) throw new System.Exception("LiftGamma is None or Volume is None");
    }

    public override void Skill()
    {
        if (_data.TimeStopSlider.value > _data.MinSliderValue)
        {
            //TODO: 시간 느리게
            Time.timeScale = 1 / _data.TimeScaleValue;
            GameManager.PlayerTimeScale = _data.TimeScaleValue;
            _isUsed = true;
            _data.TimeStopSlider.value -= Time.deltaTime * _data.MinTimeStopDuration * GameManager.PlayerTimeScale;
            _parent.ParticleActive(true);
            // if(_liftGammaGain.gamma.value == null)
            // {
            //     _gammaUpTweener.Kill();
            // }
        }
        else
        {
            Time.timeScale = 1f;
            GameManager.PlayerTimeScale = 1f;
            //TODO: 시간 원상복귀
            _parent.ParticleActive(false);
            _isUsed = false;
            if (_liftGammaGain.gamma.value == _data.GammaValue)
            {
                _liftGammaGain.gamma.Override((Vector4)Vector3.one);
            }

            if (_liftGammaGain.gain.value == _data.GainValue)
            {
                _liftGammaGain.gain.Override((Vector4)Vector3.one);
            }
        }
    }

    public void SkillInit()
    {
        _playerStat.SubSkillRatio = 0f;
    }

    public void SkillPersist()
    {
        Debug.Log(_isUsed);
        if (Input.GetKeyDown(_input.GetInput(Q)))
        {
            DOTween.To(
                () => _liftGammaGain.gamma.value,
                value => _liftGammaGain.gamma.Override(value),
                _data.GammaValue, _data.TweenDuration
            );

            DOTween.To(
                () => _liftGammaGain.gain.value,
                value => _liftGammaGain.gain.Override(value),
                _data.GainValue, _data.TweenDuration
            );
        }
        if (Input.GetKeyUp(_input.GetInput(Q)))
        {
            Time.timeScale = 1f;
            GameManager.PlayerTimeScale = 1f;
            _isUsed = false;
            _parent.ParticleActive(false);

            DOTween.To(
                () => _liftGammaGain.gamma.value,
                value => _liftGammaGain.gamma.Override(value),
                new Vector4(1f, 1f, 1f, 0f), _data.TweenDuration
            );

            DOTween.To(
                () => _liftGammaGain.gain.value,
                value => _liftGammaGain.gain.Override(value),
                new Vector4(1f, 1f, 1f, 0f), _data.TweenDuration
            );
        }
        if (!_isUsed)
        {
            _data.TimeStopSlider.value += Time.deltaTime * _data.AddTimeStopDuration * GameManager.PlayerTimeScale;
        }
    }
}

public partial class ChronosData
{
    [SerializeField]
    private float _addTimeStopDuration = 1f;
    public float AddTimeStopDuration => _addTimeStopDuration;

    [SerializeField]
    private float _minTimeStopDuration = 2f;
    public float MinTimeStopDuration => _minTimeStopDuration;

    [SerializeField]
    private Slider _timeStopSlider;
    public Slider TimeStopSlider => _timeStopSlider;

    [SerializeField]
    private Volume _globalVolume;
    public Volume GlobalVolume => _globalVolume;

    [SerializeField]
    private Vector4 _gammaValue = new Vector4(1f, 0.9f, 0.7f, 0);
    public Vector4 GammaValue => _gammaValue;

    [SerializeField]
    private float _tweenDuration = 0.5f;
    public float TweenDuration => _tweenDuration;

    [SerializeField]
    private Vector4 _gainValue = new Vector4(1f, 1f, 0.8f, 0);
    public Vector4 GainValue => _gainValue;

    [SerializeField]
    private float _timeScaleValue = 2f;
    public float TimeScaleValue => _timeScaleValue;

    [SerializeField]
    private float _minSliderValue = 0.5f;
    public float MinSliderValue => _minSliderValue;
}

