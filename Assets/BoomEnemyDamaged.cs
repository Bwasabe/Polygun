using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEnemyDamaged : BaseEnemyDamaged
{
	private BoomEnemy _testEnemy;

	protected override void Awake()
	{

	}
	protected override void Start()
	{
		_testEnemy = GetComponent<BoomEnemy>();
		RegisterStat();
		_stat.Init();
	}
	protected override void RegisterStat()
	{
		_stat = _testEnemy._stat;
	}

	public override void Damage(float damage)
	{
		_stat.Damaged(damage);
		if (_stat.HP <= 0)
		{
			Die();
		}
	}
}
