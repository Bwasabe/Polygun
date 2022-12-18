using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Yields;

public class PlayeDashSkill : BaseSkill, ISkillPersistAble
{
    private PlayerDash _dash;
    private CharacterController _cc;
    private Player _player;
    private MeshRenderer _model;

    private Vector3 _calcVelocity;
    private Vector3 _drags = Vector3.one * 8f;
    private float _dashDistance = 8f;
    private float _dahsModelTime;

    private bool _isDash = false;
    private float _dashTimer;

    private Color _startColor;
    private Color _endColor;

    private float _modelDuration;
    private List<MeshRenderer> _modelMatList = new List<MeshRenderer>();

    private List<float> _modelDurationList = new List<float>();
    public PlayeDashSkill(object parent, Color startColor, Color endColor, float modelDuration, float dashDistance, float dashModelTime, Vector3 drags) : base(null)
    {
        _dash = parent as PlayerDash;
        _cc = _dash.GetComponent<CharacterController>();
        _model = _dash.transform.Find("Model").GetComponent<MeshRenderer>();
        _player = GameManager.Instance.Player;
        _drags = drags;
        _dashDistance = dashDistance;
        _dahsModelTime = dashModelTime;
        _modelDuration = modelDuration;

        _startColor = startColor;
        _endColor = endColor;

        OnGUIManager.Instance._guiDict.Add("CalcVelocity", $"CalcVelocity : {_calcVelocity.ToString()}");
        OnGUIManager.Instance._guiDict.Add("CalcVelocityMag", $"CalcVelocity : {_calcVelocity.magnitude.ToString()}");
    }

    public override void Skill()
    {
        _isDash = true;

        _player.GetPlayerComponent<PlayerDamaged>().StopInvinciblePlayer();

        Vector3 dir;
        if (_player.CurrentState.HasFlag(PLAYER_STATE.MOVE))
            dir = _player.transform.forward;
        else
            dir = Utils.VCam.transform.forward;

        _player.CurrentState |= PLAYER_STATE.INVINCIBLE;

        _calcVelocity +=
        Vector3.Scale(dir, _dashDistance * GameManager.PlayerTimeScale *
        new Vector3(
            (Mathf.Log(1f / (Time.deltaTime * _drags.x + 1)) / -Time.deltaTime),
            0,
            (Mathf.Log(1f / (Time.deltaTime * _drags.z + 1)) / -Time.deltaTime)));
    }

    public void SkillPersist()
    {
        for (int i = 0; i < _modelMatList.Count; ++i)
        {
            if (Time.time - _modelDurationList[i] >= _modelDuration || _modelMatList[i].material.color.a <= 0)
            {
                _modelMatList[i].gameObject.SetActive(false);
                _modelMatList.RemoveAt(i);
                _modelDurationList.RemoveAt(i);
            }
            else
            {
                _modelMatList[i].material.color = Color.Lerp(_startColor, _endColor, (Time.time - _modelDurationList[i]) / _modelDuration);
            }

        }

        if (!_isDash) return;
        _dashTimer += Time.deltaTime;
        if (_dashTimer >= _dahsModelTime)
        {
            _dashTimer = 0f;
            MeshRenderer model = GameObject.Instantiate(_model, _model.transform.position, _dash.transform.rotation, null);
            _modelMatList.Add(model);
            _modelDurationList.Add(Time.time);
        }

        if (_calcVelocity.magnitude <= 0.5f)
        {
            if (_player.CurrentState.HasFlag(PLAYER_STATE.INVINCIBLE))
            {
                _player.CurrentState &= ~PLAYER_STATE.INVINCIBLE;
            }
            _isDash = false;
        }



        OnGUIManager.Instance._guiDict["CalcVelocity"] = $"CalcVelocity : {_calcVelocity.ToString()}";
        OnGUIManager.Instance._guiDict["CalcVelocityMag"] = $"CalcVelocity : {_calcVelocity.magnitude.ToString()}";
        _cc.Move(_calcVelocity * Time.deltaTime);
        _calcVelocity.x /= 1 + _drags.x * Time.deltaTime * GameManager.PlayerTimeScale;
        _calcVelocity.y /= 1 + _drags.y * Time.deltaTime * GameManager.PlayerTimeScale;
        _calcVelocity.z /= 1 + _drags.z * Time.deltaTime * GameManager.PlayerTimeScale;
    }

}
