using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : BasePlayerComponent
{
    [SerializeField]
    private Transform _attackPosition;
    [SerializeField]
    private LayerMask _hitLayer;
    [SerializeField]
    private float _bulletSpeed = 25f;
    [SerializeField]
    private float _bulletRate;

    private float _rateTime;
    private void Update()
    {
        _rateTime += Time.deltaTime;
        if (Input.GetKey(_input.GetInput("MOUSELEFTBUTTON"))&& _bulletRate <= _rateTime)
        {
            _rateTime = 0;
            Attack();
        }
    }
    protected override void RegisterInput()
    {
        _input.AddInput("MOUSELEFTBUTTON", KeyCode.Mouse0);
    }

    private void Attack()
    {
        GameObject obj = ObjectPool.Instance.GetObject(PoolObjectType.PlayerBullet);
        obj.transform.position = _attackPosition.position;
        Bullet bulletObj = obj.GetComponent<Bullet>();
        bulletObj.Direction = _attackPosition.forward;
        bulletObj.Damage = _player.PlayerStat.DamageStat;
        bulletObj.HitLayer = _hitLayer;
        bulletObj.Speed = _bulletSpeed;
        CameraManager.Instance.CameraShake();
		// bulletObj.bulletType = BulletType.ENEMY;
	}
}
