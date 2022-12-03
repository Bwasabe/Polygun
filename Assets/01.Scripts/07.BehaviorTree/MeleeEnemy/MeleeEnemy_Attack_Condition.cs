using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy_Attack_Condition : BT_Condition
{
	private MeleeEnemy_Data _data;
	private Transform _player;
	public MeleeEnemy_Attack_Condition(BehaviorTree t, Transform player,MeleeEnemy_Data data,List<BT_Node> c) : base(t, c)
	{
		_data = data;
		_player = player;
	}

	public override Result Execute()
	{
		if (Vector3.Distance(_tree.transform.position, _player.position) <= _data.attackRange)
		{
			_children[0].Execute();
			return Result.SUCCESS;
		}
		else
			return Result.FAILURE;
	}
}

public partial class MeleeEnemy_Data
{
	public float attackRange;
}
