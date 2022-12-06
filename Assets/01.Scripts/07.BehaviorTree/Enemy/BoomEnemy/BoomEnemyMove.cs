using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BoomEnemyMove : BT_Node
{
	private Transform _player;
	private CharacterController _characterController;
	private BoomEnemyData _data;
	private Vector3 _velocity;
	private Animator _ani;
	public BoomEnemyMove(BehaviorTree t, List<BT_Node> c = null) : base(t, c)
	{
		_player = GameManager.Instance.Player.transform;
		_characterController = t.GetComponent<CharacterController>();
		_data = _tree.GetData<BoomEnemyData>();
		_ani = _tree.GetComponent<Animator>();
	}

	protected override void OnUpdate()
	{
		Vector3 playerNormal = _player.position - _tree.transform.position;
		if (IsGround())
		{
			_velocity = Vector3.zero;
		}
		else
		{
			_velocity.y += Physics.gravity.y * Time.deltaTime * _data.GravityScale * GameManager.TimeScale;
		}
		_characterController.Move((_velocity+playerNormal.normalized) * 3 *Time.deltaTime);
		_tree.transform.LookAt(_player);

		_ani.SetBool("walk", true);
		if (Vector3.Distance(_tree.transform.position, GameManager.Instance.Player.transform.position) > _data.MaxDis)
		{
			NodeResult = Result.RUNNING;
		}
		else
		{
			_ani.SetBool("walk", false);
			UpdateState = UpdateState.Exit;
			NodeResult = Result.SUCCESS;
		}
	}

	private bool IsGround()
	{
		Vector3 pos2 = _tree.transform.position + _characterController.center;
		float value = _characterController.height * 0.5f - _characterController.radius;

		pos2.y -= value + _characterController.skinWidth + 0.1f;
		return Physics.CheckSphere(pos2, _characterController.radius, _data.GroundLayer);
	}
}

public partial class BoomEnemyData
{
	[SerializeField]
	private LayerMask _groundLayer;
	[SerializeField]
	private float _gravityScale;

	public float GravityScale => _gravityScale;
	public LayerMask GroundLayer => _groundLayer;
}
