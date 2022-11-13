using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType : int
{
	NONE = -1,
	ENEMY,
	PLAYER
}
public class Bullet : MonoBehaviour
{
	public int damage;
	public Vector3 foward;
	public float speed;
	public BulletType bulletType;

	[SerializeField]
	private string[] _BulletHitTag;

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

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag(_BulletHitTag[(int)bulletType]))
		{
			other.GetComponent<IDmgAble>().Damage(damage);
			ObjectPool.Instance.ReturnObject(PoolObjectType.PlayerBullet, this.gameObject);
		}
		else
		{
			ObjectPool.Instance.ReturnObject(PoolObjectType.PlayerBullet, this.gameObject);
		}
	}
}
