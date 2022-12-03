using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Attack : BT_Node
{
	private MeleeEnemy_Data _data;
	public void EndAttackAnimation()
	{
		UpdateState = UpdateState.Exit;
	}
	public Melee_Attack(BehaviorTree t, MeleeEnemy_Data data,List<BT_Node> c = null) : base(t, c)
	{
		_data = data;
	}

	protected override void OnEnter()
	{
		base.OnEnter();
		_data.Animator.SetTrigger("IsAttack");
	}
	public override Result Execute()
	{
		return base.Execute();
	}
}

public partial class MeleeEnemy_Data
{
	[SerializeField]
	private Animator _ani;

	public Animator Animator => _ani;
}
