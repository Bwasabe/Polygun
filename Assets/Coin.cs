using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Coin : Item
{
	protected override void Interaction(Collider other)
	{
		if(IsInteraction(other))
		{
			GameManager.Instance.CoinAmount++;
			ObjectPool.Instance.ReturnObject(PoolObjectType.Coin, this.gameObject);
		}
	}
}
