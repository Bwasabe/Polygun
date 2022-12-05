using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongEnemyDamaged : BaseEnemyDamaged
{
	private LongEnemy _testEnemy;

	protected override void Awake()
	{

	}
	protected override void Start()
	{
		_testEnemy = GetComponent<LongEnemy>();
		//base.Start();
		RegisterStat();
		_stat.Init();
	}
	protected override void RegisterStat()
	{
		_stat = _testEnemy.longEnemyData;
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
