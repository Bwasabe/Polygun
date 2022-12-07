using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubSkill : BasePlayerSkillComponent
{
    private const string Q = "Q";


    private float _timer;

    private void Update() {
        if(_skill == null)return;
        _timer += Time.deltaTime * GameManager.PlayerTimeScale;
        if(Input.GetKey(_input.GetInput(Q)) && _timer >= _player.PlayerStat.SubSkillRatio)
        {
            _timer = 0f;
            _skill?.Skill();
        }
    }


    public void ResetTimer()
    {
        _timer = 0f;
    }

    protected override void RegisterInput()
    {
        _input.AddInput(Q, KeyCode.Q);
    }


}
