using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Attack : BT_Node
{
	private MeleeEnemy_Data _data;

	private CollisionFlags _collisionFlag;

	private CharacterController _ch;
	public void EndAttackAnimation()
	{
		_data.Animator.SetBool("IsAttack", false);
		NodeResult = Result.SUCCESS;
		UpdateState = UpdateState.Exit;
	}
	public Melee_Attack(BehaviorTree t,MeleeEnemy_Data data,List<BT_Node> c = null) : base(t, c)
	{
		_data = data;
		_ch = _tree.GetComponent<CharacterController>();
	}

	protected override void OnEnter()
	{
		MeleeEnemy enemy = _tree as MeleeEnemy;
		enemy.endAnimation = EndAttackAnimation;
		_data.Animator.SetBool("IsAttack",true);
		NodeResult = Result.RUNNING;
		base.OnEnter();
	}

	protected override void OnUpdate()
	{
		Vector3 dir = Vector3.zero;

		if ((_collisionFlag & CollisionFlags.Below) != 0)
		{
			dir.y = 0f;
			Debug.Log("���� ����");
		}
		else
		{
			Debug.Log("�߷� ����");
			dir.y = Physics.gravity.y * Time.deltaTime;
		}

		_collisionFlag = _ch.Move(dir * _data.GravityScale * Time.deltaTime);
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
