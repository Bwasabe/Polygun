using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : BehaviorTree
{
	[SerializeField]
	private MeleeEnemy_Data _data;
	[SerializeField]
	private Transform _target;

	public Action endAnimation;
	protected override BT_Node SetupTree()
    {
		_root = new BT_Selector(this, new List<BT_Node>
		{
			new MeleeEnemy_Attack_Condition(this, _target.transform, _data, new List<BT_Node>{new Melee_Attack(this,_data)}),
			new Melee_Follow_Condition(this, _data,new List<BT_Node>{new MeleeFollow(this, _target.transform) })
		});

		return _root;
    }
	protected override void Start()
	{
		_data.Stat.Init();
		base.Start();
	}
	public void ExecuteAction()
	{
		endAnimation.Invoke();
	}
}

public partial class MeleeEnemy_Data
{
	public BT_ListRandomNode CurrentRandomNode { get; set; }
}

