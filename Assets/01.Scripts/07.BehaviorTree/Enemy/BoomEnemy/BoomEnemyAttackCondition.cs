using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class BoomEnemyAttackCondition : BT_Condition
{
	private BoomEnemyData _data;
	public BoomEnemyAttackCondition(BehaviorTree t, List<BT_Node> c) : base(t, c)
	{
		_data = _tree.GetData<BoomEnemyData>();
	}

	public override Result Execute()
	{
		base.Execute();
		if (Vector3.Distance(_tree.transform.position, GameManager.Instance.Player.transform.position) <= _data.MaxDis)
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
