using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class MeleeFollow : BT_Node
{
    private MeleeEnemy_Data _data;
    private Transform _player;
    private CharacterController ch;
	private Vector3 _velocity;
	public MeleeFollow(BehaviorTree t,  Transform player,List<BT_Node> c = null) : base(t, c)
    {
        _player = player;
		_data = _tree.GetData<MeleeEnemy_Data>();
		ch = _tree.GetComponent<CharacterController>();
	}

    protected override void OnUpdate()
    {

        Vector3 playerNormal = _player.transform.position - _tree.transform.position;
		playerNormal.y = _tree.transform.position.y;

        Vector3 player = _player.transform.position;
        player.y = _tree.transform.position.y;

		Vector3 rotation = _player.transform.position - _tree.transform.position;

		rotation.Normalize();



		if(IsGround())
		{
			_velocity = Vector3.zero;
		}
		else
		{
			_velocity.y += Physics.gravity.y * Time.deltaTime * _data.GravityScale * GameManager.TimeScale;
		}
		playerNormal += _velocity;

		ch.Move(playerNormal.normalized * Time.deltaTime * _data.Stat.Speed);
		_tree.transform.rotation = Quaternion.Slerp(_tree.transform.rotation, Quaternion.LookRotation(rotation), Time.deltaTime * 5);

		_data.Animator.SetBool("IsWalk", true);
        NodeResult = Result.RUNNING;
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
	private bool IsGround()
	{
		Vector3 pos2 = _tree.transform.position + ch.center;
		float value = ch.height * 0.5f - ch.radius;

		pos2.y -= value + ch.skinWidth + 0.1f;
		return Physics.CheckSphere(pos2, ch.radius, _data.groundLayer);
	}
	public override Result Execute()
    {
        return base.Execute();
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
