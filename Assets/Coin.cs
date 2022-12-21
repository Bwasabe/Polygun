using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Coin : Item
{
	[SerializeField]
	private AudioClip _coinClip;
	protected override void Interaction(Collision other)
	{
		if(IsInteraction(other))
		{
			SoundManager.Instance.Play(AudioType.SFX, _coinClip);
			GameManager.Instance.CoinAmount++;
			ObjectPool.Instance.ReturnObject(PoolObjectType.Coin, this.gameObject);
		}
	}
}
