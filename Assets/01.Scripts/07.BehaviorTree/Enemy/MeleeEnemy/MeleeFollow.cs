using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class MeleeFollow : BT_Node
{
    private MeleeEnemy_Data _data;
    private Transform _player;
    private CharacterController ch;

	private CollisionFlags _collisionFlag;
	public MeleeFollow(BehaviorTree t,  Transform player,List<BT_Node> c = null) : base(t, c)
    {
        _player = player;
		_data = _tree.GetData<MeleeEnemy_Data>();
		ch = _tree.GetComponent<CharacterController>();
	}
	protected override void OnUpdate()
    {

        Vector3 playerNormal = _player.transform.position - _tree.transform.position;
		playerNormal.y = 0;

		Vector3 rotation = _player.transform.position - _tree.transform.position;

		rotation.Normalize();

		Vector3 dir = GameManager.Instance.Player.transform.position - _tree.transform.position;
		dir.y = 0f;
		dir.Normalize();

		if ((_collisionFlag & CollisionFlags.Below) != 0)
		{
			dir.y = 0f;
			Debug.Log("���� ����");
		}
		else
		{
			Debug.Log("�߷� ����");
			dir.y = Physics.gravity.y * Time.deltaTime * _data.GravityScale;
		}

		_collisionFlag = ch.Move(dir * _data.Stat.Speed * Time.deltaTime);

		//_tree.transform.rotation = Quaternion.Slerp(_tree.transform.rotation, Quaternion.LookRotation(rotation), Time.deltaTime * 5);

		_data.Animator.SetBool("IsWalk", true);
		if (Vector3.Distance(_tree.transform.position, _player.position) <= _data.attackRange)
		{
			UpdateState = UpdateState.Exit;
			NodeResult = Result.SUCCESS;
		}
	}

	protected override void OnExit()
    {
		_data.Animator.SetBool("IsWalk", false);
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
	private LayerMask _groundLayer;
	[SerializeField]
	private float _gravityScale;

	public float GravityScale => _gravityScale;
	public LayerMask groundLayer => _groundLayer;
}
