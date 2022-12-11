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
        return base.Execute();
    }

    protected override void OnEnter()
    {
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

        if(Vector3.Distance(_data.Target.position, _tree.transform.position) <= _data.AttackDistance)
        {
            NodeResult = Result.FAILURE;
            UpdateState = UpdateState.None;
        }
        else
        {
            Vector3 dir = _data.Target.position - _tree.transform.position;
            dir.Normalize();
            _cc.Move(dir * _data.MoveSpeed * Time.deltaTime);
            // _agent.SetDestination(_data.Target.position);
            // _cc.Move(_agent.velocity);
        }
    }
}

public partial class AmonData
{
    public bool IsFollow { get; set; } = false;
    // public NavMeshAgent Agent{ get; set; }
    public CharacterController CC{ get; set; }

    [SerializeField]
    private float _moveSpeed = 20f;
    public float MoveSpeed => _moveSpeed;
}
