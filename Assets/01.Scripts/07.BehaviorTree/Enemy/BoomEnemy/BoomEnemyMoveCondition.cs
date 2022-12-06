using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEnemyMoveCondition : BT_Condition
{
	private BoomEnemyData _data;
	public BoomEnemyMoveCondition(BehaviorTree t, List<BT_Node> c) : base(t, c)
	{
		_data = _tree.GetData<BoomEnemyData>();
	}

	public override Result Execute()
	{
		base.Execute();
		if (Vector3.Distance(_tree.transform.position, GameManager.Instance.Player.transform.position) > _data.MaxDis)
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

public partial class BoomEnemyData
{
	[SerializeField]
	private float _maxDis;

	public float MaxDis => _maxDis;
}
