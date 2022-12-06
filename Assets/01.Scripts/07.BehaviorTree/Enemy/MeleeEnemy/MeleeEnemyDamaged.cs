using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class MeleeEnemyDamaged : BaseEnemyDamaged
{
	private MeleeEnemy _testEnemy;

	protected override void Awake()
	{

	}
	protected override void Start()
	{
		_testEnemy = GetComponent<MeleeEnemy>();
		//base.Start();
		RegisterStat();
		_stat.Init();
	}
	protected override void RegisterStat()
	{
		_stat = _testEnemy.stat;
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
