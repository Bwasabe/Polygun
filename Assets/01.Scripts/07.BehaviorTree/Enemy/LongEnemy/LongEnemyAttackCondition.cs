using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongEnemyAttackCondition : BT_Condition
{
	private LongEnemy _treeInfo;
	public LongEnemyAttackCondition(BehaviorTree t,List<BT_Node> c) : base(t, c)
	{
		_treeInfo = _tree as LongEnemy;
	}

	public override Result Execute()
	{
		base.Execute();
		if (_treeInfo.IsAttack)
		{
			_children[0].Execute();
			return Result.SUCCESS;
		}
		else
		{
			return Result.FAILURE;
		}
	}
}
