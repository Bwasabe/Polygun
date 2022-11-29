using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : BasePlayerComponent
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

    private float _rateTime;

    private PlayerStat _playerStat;

    private BaseSkill _attack;

    public float BulletSpeed => _bulletSpeed;
    public LayerMask HitLayer => _hitLayer;
    public float Damage => _player.PlayerStat.DamageStat;

    protected override void Start()
    {
        base.Start();
        _playerStat = _player.PlayerStat;
        if(_attack == null)
            _player.GetPlayerComponent<PlayerSkillCtrl>().AddPlayerSkill(new PlayerDefaultAttack(this, SKillType.MainAttack));
    }
    private void Update()
    {
        _rateTime += Time.deltaTime;
        if (Input.GetKey(_input.GetInput("MOUSE_LEFTBUTTON")) && _bulletRate <= _rateTime)
        {
            _player.CurrentState |= PLAYER_STATE.ATTACK;
            _rateTime = 0;
            _attack.Skill();
        }

        if(_rateTime >= _attackStateRate)
        {
            _player.CurrentState &= ~PLAYER_STATE.ATTACK;
        }
    }

    public void SetPlayerSkill(BaseSkill skill)
    {
        _attack = skill;
    }
    protected override void RegisterInput()
    {
        _input.AddInput("MOUSE_LEFTBUTTON", KeyCode.Mouse0);
    }

}
