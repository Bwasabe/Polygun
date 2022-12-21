using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using DG.Tweening;

public class ChronosSubSkill : BaseSkill, ISkillInitAble, ISkillPersistAble
{
    enum ChronosType
    {
        None,
        UseEnter,
        Use,
        FillEnter,
        Fill

    }
    private PlayerStat _playerStat;
    private ChronosData _data;

    private bool _isCanUse = true;

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

        if (!GameManager.Instance.GlobalVolume.profile.TryGet(out _liftGammaGain)) throw new System.Exception("LiftGamma is None or Volume is None");
    }

    public override void Skill()
    {
        if (_isCanUse)
        {
            Debug.Log("눌림");
            _type = ChronosType.UseEnter;
        }
    }

    public void SkillInit()
    {
        _playerStat.SubSkillRatio = 0f;
    }

    public void SkillPersist()
    {

        if (_type.Equals(ChronosType.UseEnter))
        {
            _watchCover.DOLocalRotateQuaternion(Quaternion.Euler(_data.WatchOpenRotation, 0f, 0f), _data.WatchCoveredDuration);
            _isCanUse = false;
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
            _parent.StartCoroutine(ChangePitch(1 / _data.TimeScaleValue));
            _parent.ParticleActive(true);
            _type = ChronosType.Use;
        }
        if (_type.Equals(ChronosType.Use))
        {
            if (_data.TimeStopSlider.value > 0.1f)
            {
                Time.timeScale = 1 / _data.TimeScaleValue;
                GameManager.PlayerTimeScale = _data.TimeScaleValue;
                _data.TimeStopSlider.value -= Time.deltaTime * _data.MinTimeStopDuration * GameManager.PlayerTimeScale;
            }
            else
            {
                _type = ChronosType.FillEnter;
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
            _parent.ParticleActive(false);
            _data.RingParticle.SetActive(false);
            _type = ChronosType.Fill;

            _parent.StartCoroutine(ChangePitch(1f));
        }
        if (_type.Equals(ChronosType.Fill))
        {
            Time.timeScale = 1f;
            GameManager.PlayerTimeScale = 1f;
            _data.TimeStopSlider.value += Time.deltaTime * _data.AddTimeStopDuration * GameManager.PlayerTimeScale;
            if (_data.TimeStopSlider.value >= _data.MinSliderValue && !_isCanUse)
            {
                    _isCanUse = true;
                    _data.RingParticle.SetActive(true);
                // _watchCover.DOLocalRotateQuaternion(Quaternion.Euler(_data.WatchOpenRotation, 0f, 0f), _data.WatchCoveredDuration).OnComplete(() =>
                // {
                // });

            }
        }
    }

    private IEnumerator ChangePitch(float value)
    {
        
        float timer = 0f;
        while(timer <= _data.PitchChangeDuration)
        {
            timer += Time.deltaTime;
            SoundManager.Instance.SetAllSourcePitch(
                Mathf.Lerp(
                    SoundManager.Instance.GetPitch(AudioType.BGM), value, timer / _data.PitchChangeDuration
            ));
            yield return null;
        }
        timer = 0f;
    }
}

public partial class ChronosData
{
    [SerializeField]
    private GameObject _ringParticle;
    public GameObject RingParticle => _ringParticle;

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
    private float _watchOpenRotation = 30f;
    public float WatchOpenRotation => _watchOpenRotation;

    [SerializeField]
    private float _watchCoveredDuration = 0.3f;
    public float WatchCoveredDuration => _watchCoveredDuration;

    [SerializeField]
    private float _pitchChangeSmooth = 3f;
    public float PitchChangeDuration => _pitchChangeSmooth;
}

