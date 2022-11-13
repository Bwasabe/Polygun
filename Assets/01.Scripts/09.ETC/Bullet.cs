using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType : int
{
	NONE = -1,
	ENEMY,
	PLAYER
}

[RequireComponent(typeof(ParticleSystem))]
public class Bullet : MonoBehaviour
{
	public int damage;
	public Vector3 foward;
	public float speed;
	

    [SerializeField]
    private LayerMask _hitLayer;

    private ParticleSystem particleSystem;

	private void Awake()
	{
		particleSystem = GetComponent<ParticleSystem>();
	}
	private void Update()
	{
		this.transform.position += foward.normalized * speed * Time.deltaTime;
		if (!particleSystem.IsAlive())
			ObjectPool.Instance.ReturnObject(PoolObjectType.PlayerBullet, this.gameObject);
	}

	protected virtual void Hit(Collider other)
	{
		if( ((1 << other.gameObject.layer) & _hitLayer) > 0 )
		{
			other.GetComponent<IDmgAble>()?.Damage(damage);
			ObjectPool.Instance.ReturnObject(PoolObjectType.PlayerBullet, this.gameObject);
		}
		else
		{
			ObjectPool.Instance.ReturnObject(PoolObjectType.PlayerBullet, this.gameObject);
		}
	}
}
