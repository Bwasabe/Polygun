using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerStat
{
	[SerializeField]
	private int _maxHp;
	[SerializeField]
	private int _damage;
	public int DamageStat => _damage;

	private int _hp;
	public int HP => _hp;

	public void ResetHp()
	{
		_hp = _maxHp;
	}

	public void Damaged(int damage)
	{
		_hp -= damage;
		Debug.Log(_hp);
	}
}
