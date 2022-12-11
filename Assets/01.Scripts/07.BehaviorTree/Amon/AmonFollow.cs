using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AmonFollow : BT_Node
{
    private CharacterController _cc;
    // private NavMeshAgent _agent;
    private AmonData _data;

    public AmonFollow(BehaviorTree t, List<BT_Node> c = null) : base(t, c)
    {
        _data = _tree.GetData<AmonData>();
        _cc = _data.CC;
        // _agent = _data.Agent;
    }

    public override Result Execute()
    {
        base.Execute();
        return NodeResult;
    }

    protected override void OnEnter()
    {
        if(_data.IsAttack)
        {
            UpdateState = UpdateState.Exit;
            return;
        }
        base.OnEnter();
        // _agent.speed = _data.MoveSpeed;
        NodeResult = Result.SUCCESS;
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.WALK);
    }

    protected override void OnExit()
    {
        base.OnExit();
    }

    protected override void OnUpdate()
    {
        if (Vector3.Distance(_data.Target.position, _tree.transform.position) <= _data.AttackDistance)
        {
            NodeResult = Result.FAILURE;
            UpdateState = UpdateState.Exit;
            _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.IDLE);
            _data.IsAttack = true;
        }
        else
        {
            NodeResult = Result.SUCCESS;

            Vector3 dir = _data.Target.position - _tree.transform.position;
            dir.y = 0f;
            dir.Normalize();

            _cc.Move(dir * _data.MoveSpeed * Time.deltaTime);

            Vector3 lookDir = _data.Target.position - _tree.transform.position;
            if (lookDir != Vector3.zero)
            {
                _tree.transform.rotation = Quaternion.Slerp(_tree.transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * _data.RotateSmooth);
                _tree.transform.rotation = Quaternion.Euler(0f, _tree.transform.eulerAngles.y, 0f);
            }
            // _agent.SetDestination(_data.Target.position);
            // _cc.Move(_agent.velocity);
        }
    }
}

public partial class AmonData
{
    public bool IsAttack { get; set; } = false;
    // public NavMeshAgent Agent{ get; set; }
    public CharacterController CC { get; set; }

    [SerializeField]
    private float _moveSpeed = 20f;
    public float MoveSpeed => _moveSpeed;

    [SerializeField]
    private float _attackDistance = 1f;
    public float AttackDistance => _attackDistance;
}
