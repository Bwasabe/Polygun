using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Melee_Follow_Condition : BT_Condition
{
	public Melee_Follow_Condition(BehaviorTree t, List<BT_Node> c) : base(t, c)
	{
	}

	public override Result Execute()
	{
		_children[0].Execute();
		return Result.SUCCESS;
	}
}
