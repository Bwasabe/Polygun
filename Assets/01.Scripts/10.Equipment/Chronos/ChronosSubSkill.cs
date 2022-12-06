using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChronosSubSkill : BaseSkill,ISkillInitAble, ISkillPersistAble
{
    private PlayerStat _playerStat;
    private ChronosData _data;

    private bool _isUsed;

    private InputManager _input;
    public ChronosSubSkill(BaseEquipment parent) : base(parent)
    {
        _playerStat = GameManager.Instance.Player.PlayerStat;
        _input = GameManager.Instance.InputManager;
        _data = _parent.GetData<ChronosData>();

        _data.TimeStopSlider.maxValue = _data.TimeStopCoolTime;
    }

    public override void Skill()
    {
        if(_data.TimeStopSlider.value > _data.TimeStopSlider.minValue)
        {
            _isUsed = true;
            _data.TimeStopSlider.value -= Time.deltaTime * _data.TimeStopCoolTime;
        }
    }

    public void SkillInit()
    {
        _playerStat.SubSkillRatio = 0f;
    }

    public void SkillPersist()
    {
        if(Input.GetKeyUp(_input.GetInput("Q")))
        {
            _isUsed = false;
        }
        if(!_isUsed)
        {
            _data.TimeStopSlider.value += Time.deltaTime;
        }
    }
}

public partial class ChronosData
{
    [SerializeField]
    private float _timeStopCoolTime = 10f;
    public float TimeStopCoolTime => _timeStopCoolTime;

    [SerializeField]
    private Slider _timeStopSlider;

    public Slider TimeStopSlider => _timeStopSlider;
}
