using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MeleeEnemy : BehaviorTree
{
	[SerializeField]
	private MeleeEnemy_Data _data;
	[SerializeField]
	private Transform _target;
	[SerializeField]
	CollisionCtrl attackCtrl;
	[SerializeField]
	private LayerMask layer;

	public bool isAttackTime;
	public Action endAnimation;
	protected override void Awake()
	{
		base.Awake();
		attackCtrl.ColliderEnterEvent += Hit;
	}
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
		isAttackTime = false;
		base.Start();
	}
	public void ExecuteAction()
	{
		endAnimation.Invoke();
		isAttackTime = false;
	}

	private void Hit(Collider other)
	{
		if (((1 << other.gameObject.layer) & layer) > 0 && _data.Animator.GetBool("IsAttack") && !isAttackTime)
		{
			isAttackTime = true;
			other.GetComponent<IDmgAble>()?.Damage(_data.Stat.DamageStat);
		}
	}
}

public partial class MeleeEnemy_Data
{
	public BT_ListRandomNode CurrentRandomNode { get; set; }
}

