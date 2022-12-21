using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPortion : Item
{
	[SerializeField]
	private float _hp;
	[SerializeField]
	private GameObject _returnObject;

	protected override void Interaction(Collider other)
	{
		if(IsInteraction(other))
		{
			Player player = other.gameObject.GetComponent<Player>();
			if(player.PlayerStat.HP < 100)
			{
				player.PlayerStat.Damaged(-_hp);
				ObjectPool.Instance.ReturnObject(PoolObjectType.MiddleHeal, _returnObject);
			}
		}
	}
}
