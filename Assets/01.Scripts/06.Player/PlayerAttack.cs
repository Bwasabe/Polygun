using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : BasePlayerSkillComponent
{
    [SerializeField]
    private Transform _attackPosition;
    public Transform AttackPos => _attackPosition;

    [SerializeField]
    private LayerMask _hitLayer;
    [SerializeField]
    private float _bulletSpeed = 25f;
    [SerializeField]
    private float _bulletRate;
    [SerializeField]
    private float _attackStateRate = 1f;

    public int ReloadCount { get; set; }
    private float _rateTime;

    private PlayerStat _playerStat;


    public float BulletSpeed => _bulletSpeed;
    public LayerMask HitLayer => _hitLayer;
    public float Damage => _player.PlayerStat.DamageStat;

    protected override void Start()
    {
        base.Start();
        _playerStat = _player.PlayerStat;
        if(_skill == null)
            _player.GetPlayerComponent<PlayerSkillCtrl>().AddPlayerSkill<PlayerAttack>(new PlayerDefaultAttack(this));
    }
    private void Update()
    {
        _rateTime += Time.deltaTime * GameManager.PlayerTimeScale;
        if (Input.GetKey(_input.GetInput("MOUSE_LEFTBUTTON")) && _bulletRate <= _rateTime)
        {
            _player.CurrentState |= PLAYER_STATE.ATTACK;
            _rateTime = 0;
            _skill.Skill();
        }

        if(_rateTime >= _attackStateRate)
        {
            _player.CurrentState &= ~PLAYER_STATE.ATTACK;
        }
    }

    public void SetBulletRate(float value)
    {
        _bulletRate = value;
    }

    protected override void RegisterInput()
    {
        _input.AddInput("MOUSE_LEFTBUTTON", KeyCode.Mouse0);
    }

}
