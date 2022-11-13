using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : BasePlayerComponent
{
	[SerializeField]
	private Transform _attackPosition;
	[SerializeField]
	private float _bulletRate;

	private float _rateTime;
	private void Update()
	{
		if (Input.GetKey(_input.GetInput("MOUSELEFTBUTTON")))
		{
			_rateTime += Time.deltaTime;
			if (_bulletRate <= _rateTime)
			{
				_rateTime = 0;
				Attack();
			}
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
		bulletObj.foward = _attackPosition.forward;
		bulletObj.damage = _player.PlayerStat.DamageStat;
		// bulletObj.bulletType = BulletType.ENEMY;
	}
}
