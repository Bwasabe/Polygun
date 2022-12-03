using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Follow_Condition : BT_Condition
{
	private MeleeEnemy_Data _data;
	public Melee_Follow_Condition(BehaviorTree t, MeleeEnemy_Data data, List<BT_Node> c) : base(t, c)
	{
		_data = data;
	}

	public override Result Execute()
	{
		if (_data.Animator.GetBool("IsAttack"))
		{
			return Result.FAILURE;
		}
		else
		{
			_children[0].Execute();
			return Result.SUCCESS;
		}
	}
}
