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

	private void Awake()
	{
		mesh = GetComponent<MeshCollider>();
		_collisionCtrl = GetComponent<CollisionCtrl>();
		_collisionCtrl.ColliderEnterEvent += Hit;
	}
	void Start()
    {
		mesh.enabled = true;
	}

	private void Hit(Collider other)
	{
		if (((1 << other.gameObject.layer) & layer) > 0)
		{
			Debug.Log("?");
			//����ٰ� �˹� ����
			mesh.enabled = false;
			other.GetComponent<IDmgAble>()?.Damage(damage);
		}
	}
}
