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

    private BaseAttack _attack;

    protected override void Start()
    {
        base.Start();
        _playerStat = _player.PlayerStat;
        _attack ??= new PlayerDefaultAttack(this);
    }
    private void Update()
    {
        _rateTime += Time.deltaTime;
        if (Input.GetKey(_input.GetInput("MOUSE_LEFTBUTTON"))&& _bulletRate <= _rateTime)
        {
            _player.CurrentState |= PLAYER_STATE.ATTACK;
            _rateTime = 0;
            _attack.Attack(_player.PlayerStat.DamageStat, _hitLayer, _bulletSpeed);
        }

        if(_rateTime >= _attackStateRate)
        {
            _player.CurrentState &= ~PLAYER_STATE.ATTACK;
        }
    }
    protected override void RegisterInput()
    {
        _input.AddInput("MOUSE_LEFTBUTTON", KeyCode.Mouse0);
    }

}
