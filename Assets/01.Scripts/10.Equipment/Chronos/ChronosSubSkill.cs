using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using DG.Tweening;

public class ChronosSubSkill : BaseSkill, ISkillInitAble, ISkillPersistAble
{
    enum ChronosType
    {
        UseEnter,
        Use,
        FillEnter,
        Fill,

    }
    private PlayerStat _playerStat;
    private ChronosData _data;

    private bool _isUsed;

    private const string Q = "Q";

    private InputManager _input;
    private LiftGammaGain _liftGammaGain;

    private Transform _watchCover;

    private ChronosType _type;

    public ChronosSubSkill(BaseEquipment parent) : base(parent)
    {
        _playerStat = GameManager.Instance.Player.PlayerStat;
        _input = GameManager.Instance.InputManager;
        _data = _parent.GetData<ChronosData>();
        _watchCover = _parent.transform.Find("Clock/WatchCover");

        // _data.TimeStopSlider.maxValue = _data.TimeStopCoolTime;

        if (!_data.GlobalVolume.profile.TryGet(out _liftGammaGain)) throw new System.Exception("LiftGamma is None or Volume is None");
    }

    public override void Skill()
    {
        if (_type.Equals(ChronosType.UseEnter))
        {
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
            Time.timeScale = 1 / _data.TimeScaleValue;
            GameManager.PlayerTimeScale = _data.TimeScaleValue;
            _parent.ParticleActive(true);

        }
        if (_type.Equals(ChronosType.Use))
        {
            if (_data.TimeStopSlider.value > _data.MinSliderValue)
            {
                _data.TimeStopSlider.value -= Time.deltaTime * _data.MinTimeStopDuration * GameManager.PlayerTimeScale;
            }
        }
        if (_type.Equals(ChronosType.FillEnter))
        {
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
            _watchCover.DOLocalRotateQuaternion(Quaternion.Euler(_data.WatchCoverRotation, 0f, 0f), _data.WatchCoveredDuration);
        }
        if (_type.Equals(ChronosType.Fill))
        {
            _data.TimeStopSlider.value += Time.deltaTime * _data.AddTimeStopDuration * GameManager.PlayerTimeScale;
        }

        if (_data.TimeStopSlider.value > _data.MinSliderValue)
        {
            //시간 느리게
            Time.timeScale = 1 / _data.TimeScaleValue;
            GameManager.PlayerTimeScale = _data.TimeScaleValue;
            _isUsed = true;
            _data.TimeStopSlider.value -= Time.deltaTime * _data.MinTimeStopDuration * GameManager.PlayerTimeScale;
            _parent.ParticleActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            GameManager.PlayerTimeScale = 1f;
            //시간 원상복귀
            _parent.ParticleActive(false);
            _isUsed = false;
            // if (_liftGammaGain.gamma.value == _data.GammaValue)
            // {
            //     _liftGammaGain.gamma.Override((Vector4)Vector3.one);
            // }

            // if (_liftGammaGain.gain.value == _data.GainValue)
            // {
            //     _liftGammaGain.gain.Override((Vector4)Vector3.one);
            // }
        }
    }

    public void SkillInit()
    {
        _playerStat.SubSkillRatio = 0f;
    }

    public void SkillPersist()
    {
        if (Input.GetKeyDown(_input.GetInput(Q)) && !_isUsed)
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

            _watchCover.DOLocalRotateQuaternion(Quaternion.Euler(_data.WatchCoverRotation, 0f, 0f), _data.WatchCoveredDuration);
        }
        if (Input.GetKeyUp(_input.GetInput(Q)))
        {
            // Time.timeScale = 1f;
            // GameManager.PlayerTimeScale = 1f;
            // _isUsed = false;
            // _parent.ParticleActive(false);


        }
        if (!_isUsed)
        {

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

    public Volume GlobalVolume { get; set; }

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

    [SerializeField]
    private float _watchCoverRotation = -95f;
    public float WatchCoverRotation => _watchCoverRotation;

    [SerializeField]
    private float _watchCoveredDuration = 0.3f;
    public float WatchCoveredDuration => _watchCoveredDuration;
}

