using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;

public class ChronosAttack : BaseSkill
{
    private PlayerAttack _attack;
    private ChronosData _data;

    private int _attackIndex = 0;
    public ChronosAttack(BaseEquipment parent) : base(parent)
    {
        _data = parent.GetData<ChronosData>();

        _attack = GameManager.Instance.Player.GetPlayerComponent<PlayerAttack>();
        _attack.SetBulletRate(_data.AttackCoolTime);

    }

    public override void Skill()
    {
        _attackIndex++;
        if (_data.LastAttackIndex - 1 == _attackIndex)
        {
            _attack.SetBulletRate(_data.LastAttackCoolTime);
        }
        else if (_data.LastAttackIndex.Equals(_attackIndex))
        {
            Attack(_data.LastDamage, Vector3.one * 2f); // 4
            _attackIndex = 0;
        }
        else
        {
            Attack(_data.Damage, Vector3.one);
            _attack.SetBulletRate(_data.AttackCoolTime);
        }

    }

    private void Attack(float damage, Vector3 scale)
    {
        GameObject obj = ObjectPool.Instance.GetObject(PoolObjectType.ChronosBullet);
        obj.transform.position = _attack.AttackPos.position;
        obj.transform.rotation = _attack.transform.localRotation;
        Bullet bulletObj = obj.GetComponent<Bullet>();
        bulletObj.Direction = _attack.AttackPos.forward;
        bulletObj.Damage = damage;
        bulletObj.HitLayer = _attack.HitLayer;
        bulletObj.Speed = _data.BulletSpeed;
        bulletObj.transform.localScale = scale;
        bulletObj.IsPlayerBullet = true;
        CameraManager.Instance.CameraShake();
    }
}

public partial class ChronosData
{
    [SerializeField]
    private float _damage = 7f;
    public float Damage => _damage;

    [SerializeField]
    private float _lastDamage = 20f;
    public float LastDamage => _lastDamage;

    [SerializeField]
    private float _bulletSpeed = 40f;
    public float BulletSpeed => _bulletSpeed;

    [SerializeField]
    private float _attackCoolTime = 0.1f;
    public float AttackCoolTime => _attackCoolTime;

    [SerializeField]
    private float _lastAttackCoolTime = 0.4f;
    public float LastAttackCoolTime => _lastAttackCoolTime;

    [SerializeField]
    private int _lastAttackIndex = 5;
    public int LastAttackIndex => _lastAttackIndex;
}


