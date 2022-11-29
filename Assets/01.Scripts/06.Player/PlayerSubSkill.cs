using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubSkill : BasePlayerComponent
{
    private const string LSHIFT = "LSHIFT";
    private BaseSkill _skill;


    private float _timer;

    protected override void Start()
    {
        base.Start();
        if(_skill == null)
        {
            _player.GetPlayerComponent<PlayerSkillCtrl>().AddPlayerSkill(new PlayerDefaultSubSkill(this, SKillType.SubSkill));
        }
    }
    private void Update() {
        _timer += Time.deltaTime;
        if(Input.GetKey(_input.GetInput(LSHIFT)) && _timer >= _player.PlayerStat.SubSkillRatio)
        {
            _timer = 0f;
            _skill.Skill();
        }
    }


    public void ResetTimer()
    {
        _timer = 0f;
    }

    protected override void RegisterInput()
    {
        _input.AddInput(LSHIFT, KeyCode.LeftShift);
    }

    public void SetPlayerSkill(BaseSkill skill)
    {
        _skill = skill;
    }

}
