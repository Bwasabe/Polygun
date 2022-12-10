using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Coin : MonoBehaviour
{
	private CollisionCtrl _collision;

	[SerializeField]
	private LayerMask _layerMask;
	private void Awake()
	{
		_collision = GetComponent<CollisionCtrl>();
		_collision.ColliderEnterEvent += CoinGet;
	}

	private void CoinGet(Collider other)
	{
		if (((1 << other.gameObject.layer) & _layerMask) > 0)
		{
			GameManager.Instance.CoinAmount++;
			ObjectPool.Instance.ReturnObject(PoolObjectType.Coin,this.gameObject);
		}
	}
}
