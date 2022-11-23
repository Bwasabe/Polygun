using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShorkWaveCollision : MonoBehaviour
{
	[NonSerialized]
	public float damage;

	[SerializeField]
	private LayerMask layer;
	private CollisionCtrl _collisionCtrl;
	void Start()
    {
        _collisionCtrl = GetComponent<CollisionCtrl>();
		_collisionCtrl.ColliderEnterEvent += Hit;
	}

	private void Hit(Collider other)
	{
		if(((1 << other.gameObject.layer) & layer) > 0)
		{
			Debug.Log(damage);
			other.GetComponent<IDmgAble>()?.Damage(damage);
		}
	}
}
