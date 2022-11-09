using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : BasePlayerComponent,IDmgAble, IDmgCallbackable
{
	protected override void Start()
	{
		base.Start();
		_player.playerStat.ResetHp();
	}
	public void Damage(int damage)
	{
		_player.playerStat.Damaged(damage);
		if (_player.playerStat.HP <= 0)
		{
			Die();
		}
	}

	public void Damage(Collider other)
	{
	}

	private void Die()
	{
		GameManager.Instance.Player.CurrentState = PLAYER_STATE.DIE;
		this.gameObject.SetActive(false);
	}

	protected override void RegisterInput()
	{
	}
}
