using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefaultSubSkill : BaseSkill, ISkillPersistAble
{
    private PlayerSubSkill _parent;
    private CharacterController _cc;

    private Vector3 _calcVelocity;
    private Vector3 _drags = Vector3.one * 8f;
    private float _dashDistance = 8f;
    private Player _player;

    public PlayerDefaultSubSkill(object parent, SKillType sKillType) : base(sKillType)
    {
        _parent = parent as PlayerSubSkill;
        _cc = _parent.GetComponent<CharacterController>();
        OnGUIManager.Instance._guiDict.Add("CalcVelocity", $"CalcVelocity : {_calcVelocity.ToString()}");
        OnGUIManager.Instance._guiDict.Add("CalcVelocityMag", $"CalcVelocity : {_calcVelocity.magnitude.ToString()}");
        _player = GameManager.Instance.Player;
    }

    public override void Skill()
    {
        Vector3 dir;
        if(_player.CurrentState.HasFlag(PLAYER_STATE.MOVE))
            dir = _player.transform.forward;
        else
            dir = Utils.VCam.transform.forward;
        _player.CurrentState |= PLAYER_STATE.INVINCIBLE;
        _calcVelocity +=
        Vector3.Scale(dir, _dashDistance *
        new Vector3(
            (Mathf.Log(1f / (Time.deltaTime * _drags.x + 1)) / -Time.deltaTime),
            0,
            (Mathf.Log(1f / (Time.deltaTime * _drags.z + 1)) / -Time.deltaTime)));
    }

    public void SkillPersist()
    {
        if(_calcVelocity.magnitude <= 1f && _player.CurrentState.HasFlag(PLAYER_STATE.INVINCIBLE))
        {
            _player.CurrentState &= ~PLAYER_STATE.INVINCIBLE;
        }
        OnGUIManager.Instance._guiDict["CalcVelocity"] = $"CalcVelocity : {_calcVelocity.ToString()}";
        OnGUIManager.Instance._guiDict["CalcVelocityMag"] = $"CalcVelocity : {_calcVelocity.magnitude.ToString()}";
        _cc.Move(_calcVelocity * Time.deltaTime);
        _calcVelocity.x /= 1 + _drags.x * Time.deltaTime;
        _calcVelocity.y /= 1 + _drags.y * Time.deltaTime;
        _calcVelocity.z /= 1 + _drags.z * Time.deltaTime;
    }

}
