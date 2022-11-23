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
	private MeshCollider mesh;

	void Start()
    {
		mesh = GetComponent<MeshCollider>();
		_collisionCtrl = GetComponent<CollisionCtrl>();
		mesh.enabled = true;
		_collisionCtrl.ColliderEnterEvent += Hit;
	}

	private void Hit(Collider other)
	{
		Debug.Log(other.gameObject.layer);
		if (((1 << other.gameObject.layer) & layer) > 0)
		{
			Debug.Log("?");
			//여기다가 넉백 구현
			mesh.enabled = false;
			other.GetComponent<IDmgAble>()?.Damage(damage);
		}
	}
}
