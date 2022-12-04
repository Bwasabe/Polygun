using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.Search;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Melee_Attack : BT_Node
{
	private MeleeEnemy_Data _data;
	public void EndAttackAnimation()
	{
		UpdateState = UpdateState.Exit;
	}
	public Melee_Attack(BehaviorTree t,MeleeEnemy_Data data,List<BT_Node> c = null) : base(t, c)
	{
		_data = data;
	}

	protected override void OnEnter()
	{
		MeleeEnemy enemy = _tree as MeleeEnemy;
		enemy.endAnimation = EndAttackAnimation;
		_data.Animator.SetBool("IsAttack",true);
		base.OnEnter();
	}

	protected override void OnUpdate()
	{
		NodeResult = Result.RUNNING;
	}

	protected override void OnExit()
	{
		_data.Animator.SetBool("IsAttack", false);
		NodeResult = Result.SUCCESS;
		base.OnExit();
	}

	public override Result Execute()
	{
		base.Execute();
		return NodeResult;
	}
}

public partial class MeleeEnemy_Data
{
	[SerializeField]
	private Animator _ani;

	public Animator Animator => _ani;
}
