using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : BasePlayerComponent,IDmgAble, IDmgCallbackable
{
	protected override void Start()
	{
		base.Start();
		_player.PlayerStat.ResetHp();
	}
	public void Damage(int damage)
	{
		_player.PlayerStat.Damaged(damage);
		if (_player.PlayerStat.HP <= 0)
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
