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

    [SerializeField]
    private ParticleSystem _flush;
    [SerializeField]
    private ParticleSystem _hit;

    private ParticleSystem _particleSystem;

    private CollisionCtrl _collisionCtrl;

    protected virtual void Awake()
	{
		_particleSystem = GetComponent<ParticleSystem>();
        _collisionCtrl = GetComponent<CollisionCtrl>();
    }

	protected virtual void Start() {
        _collisionCtrl.ColliderEnterEvent += Hit;
    }

	protected virtual void Update()
	{
		this.transform.position += Direction.normalized * Speed * Time.deltaTime;
		if (!_particleSystem.IsAlive())
			ObjectPool.Instance.ReturnObject(PoolObjectType.PlayerBullet, this.gameObject);
	}


	protected virtual void Hit(Collider other)
	{
		if( ((1 << other.gameObject.layer) & HitLayer) > 0 )
		{
			other.GetComponent<IDmgAble>()?.Damage(Damage);
			// if(_hit != null)
			// {
			// 	GameObject hit = ObjectPool.Instance.GetObject(PoolObjectType.FireBullet_Flush);
            //     ParticleSystem particle = hit.GetComponent<ParticleSystem>();

            //     StartCoroutine(ObjectPool.Instance.ReturnObject(PoolObjectType.FireBullet_Hit, hit, particle.main.duration));
            // }

			// if(_flush != null)
			// {
				
			// }
		}
		ObjectPool.Instance.ReturnObject(PoolObjectType.PlayerBullet, this.gameObject);
	}

}
