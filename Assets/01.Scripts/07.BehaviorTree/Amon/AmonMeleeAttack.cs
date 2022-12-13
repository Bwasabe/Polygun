using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Yields;

public class AmonMeleeAttack : BT_Node
{
    private AmonData _data;
    private const string MELEE_ATTACK = "attack_01";

    private float _timer;
    private int _currentBulletIndex = 0;
    private float _bulletTime;
    private bool _isSummonBullet;

    public AmonMeleeAttack(BehaviorTree t, List<BT_Node> c = null) : base(t, c)
    {
        _data = _tree.GetData<AmonData>();
    }

    public override Result Execute()
    {
        base.Execute();
        return NodeResult;
    }

    protected override void OnEnter()
    {
        NodeResult = Result.RUNNING;
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.MELEE_ATTACK);
        base.OnEnter();
        _data.IsAttack = true;
    }

    protected override void OnUpdate()
    {
        _timer += Time.deltaTime;
        if (_isSummonBullet)
        {
            if (_timer >= _data.MeleeBulletSpawnDuration)
            {
                _timer = 0f;
                if (_currentBulletIndex < _data.MeleeBulletPos.Count)
                {
                    Vector3 dir = _data.Target.position - _data.MeleeBulletPos[_currentBulletIndex].position;
                    dir.Normalize();
                    _data.MeleeBullets[_currentBulletIndex].Direction = dir;
                    _data.MeleeBullets[_currentBulletIndex].Damage = _data.MeleeBulletDamage;
                    _data.MeleeBullets[_currentBulletIndex].Speed = _data.MeleeBulletSpeed;
                    _currentBulletIndex++;
                }
                else
                {
                    _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.MELEE_ATTACK_END);
                    NodeResult = Result.SUCCESS;
                    UpdateState = UpdateState.Exit;
                    _data.IsAttack = false;
                }

            }
        }
        else
        {
            if (_timer >= _data.AnimatorCtrl.GetAnimationLength(MELEE_ATTACK))
            {
                _isSummonBullet = true;
                _timer = 0f;
            }
        }
    }

    protected override void OnExit()
    {
        _timer = 0f;
        _isSummonBullet = false;
        _currentBulletIndex = 0;

        base.OnExit();
    }
}

public partial class AmonData
{
    [SerializeField]
    private float _attackDelay = 1f;
    public float AttackDelay => _attackDelay;

    [SerializeField]
    private float _meleeBulletSpeed = 30f;
    public float MeleeBulletSpeed => _meleeBulletSpeed;

    [SerializeField]
    private float _meleeBulletDamage = 10f;
    public float MeleeBulletDamage => _meleeBulletDamage;
}
