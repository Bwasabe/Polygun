using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour, IDmgAble
{
	[SerializeField]
	private float _bulletRate;

	private float _rateTime;
	private void Update()
	{
		_rateTime += Time.deltaTime;
		if (_bulletRate <= _rateTime)
		{
			_rateTime = 0;
			Attack();
		}
	}
	public int a;
	public void Damage(int damage)
	{
		a -= damage;
		Debug.Log(a);
	}

	private void Attack()
	{
		GameObject obj = ObjectPool.Instance.GetObject(PoolObjectType.PlayerBullet);
		obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y-4, this.transform.position.z - 1f);
		Bullet bulletObj = obj.GetComponent<Bullet>();
		bulletObj.foward = -this.transform.forward;
		bulletObj.damage = 10;
		// bulletObj.bulletType = BulletType.PLAYER;
	}
}
