using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Yields;

public enum BulletType : int
{
	NONE = -1,
	ENEMY,
	PLAYER
}

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(CollisionCtrl))]
public class Bullet : MonoBehaviour
{
    public int Damage{ get; set; }
	public Vector3 Direction{ get; set; }
    public float Speed{ get; set; }

    public LayerMask HitLayer{ get; set; }

	// [SerializeField]
	// private ParticleSystem _flush;
	// [SerializeField]
	// private ParticleSystem _hit;

	private ParticleSystem _particleSystem;

    private CollisionCtrl _collisionCtrl;

	public GameObject flash;

	private GameObject _flashObj;

    protected virtual void Awake()
	{
		_particleSystem = GetComponent<ParticleSystem>();
        _collisionCtrl = GetComponent<CollisionCtrl>();
		_collisionCtrl.ColliderEnterEvent += Hit;
    }

	private void Start()
	{
		if (_flashObj != null)
			DoFlash();
		else
		{
			_flashObj = Instantiate(flash, transform);
			DoFlash();
		}
	}
	protected virtual void OnEnable() {
    }

	protected virtual void Update()
	{
		this.transform.position += Direction.normalized * Speed * Time.deltaTime;
		if (!_particleSystem.IsAlive())
		{
			ObjectPool.Instance.ReturnObject(PoolObjectType.PlayerBullet, this.gameObject);
		}
	}
	void DoFlash()
	{
		_flashObj.transform.forward = gameObject.transform.forward;
		var flashPs = _flashObj.GetComponent<ParticleSystem>();
		flashPs.Play();
	}

	protected virtual void Hit(Collider other)
	{
		if( ((1 << other.gameObject.layer) & HitLayer) > 0 )
		{
			other.GetComponent<IDmgAble>()?.Damage(Damage);
		}

		ObjectPool.Instance.ReturnObject(PoolObjectType.PlayerBullet, this.gameObject);
	}
}
