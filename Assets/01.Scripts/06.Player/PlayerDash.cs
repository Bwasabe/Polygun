using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : BasePlayerSkillComponent
{
    private const string LSHIFT = "LSHIFT";

    private float _timer;

    public int MaxDashCount { get; set; } = 1;
    private int _currentDashCount = 0;


    [SerializeField]
    private Vector3 _drags = Vector3.one * 8f;

    [SerializeField]
    private float _dashDistance = 8f;
    [SerializeField]
    private float _dashSpawnDuration = 0.001f;
    [SerializeField]
    private float _modelDuration = 1f;

    [SerializeField]
    private Color _startColor;
    [SerializeField]
    private Color _endColor;

    protected override void Start()
    {
        base.Start();
        if (_skill == null)
        {
            _player.GetPlayerComponent<PlayerSkillCtrl>().AddPlayerSkill<PlayerDash>(
                new PlayeDashSkill(this, _startColor, _endColor, _modelDuration, _dashDistance, _dashSpawnDuration, _drags));
        }
    }
    private void Update()
    {
        if (_currentDashCount < MaxDashCount)
        {
            _timer += Time.deltaTime * GameManager.PlayerTimeScale;
            if (_timer >= _player.PlayerStat.DashRatio)
            {
                _timer = 0f;
                _currentDashCount++;
            }
        }

        if (Input.GetKey(_input.GetInput(LSHIFT)) && _currentDashCount > 0)
        {
            if (_skill == null)
            {
                _player.GetPlayerComponent<PlayerSkillCtrl>().AddPlayerSkill<PlayerDash>(
                    new PlayeDashSkill(this, _startColor, _endColor, _modelDuration, _dashDistance, _dashSpawnDuration, _drags));
            }
            _currentDashCount--;
            _skill.Skill();
        }
    }

    protected override void RegisterInput()
    {
        _input.AddInput(LSHIFT, KeyCode.LeftShift);
    }

}
