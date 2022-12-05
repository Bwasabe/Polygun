using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongEnemyMoveCondition : BT_Condition
{
	LongEnemy longEnemy;
	public LongEnemyMoveCondition(BehaviorTree t, List<BT_Node> c) : base(t, c)
	{
		longEnemy = _tree as LongEnemy;
	}

	public override Result Execute()
	{
		_children[0].Execute();
		if (longEnemy.IsAttack)
			return Result.FAILURE;
		else
			return Result.SUCCESS;
	}
}
