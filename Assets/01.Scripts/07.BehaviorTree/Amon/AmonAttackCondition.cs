using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmonAttackCondition : BT_Condition
{
    private AmonData _data;
    public AmonAttackCondition(BehaviorTree t, List<BT_Node> c) : base(t, c)
    {
        _data = _tree.GetData<AmonData>();
    }

    public override Result Execute()
    {
        base.Execute();
        return NodeResult;
    }

    protected override void OnEnter()
    {
        base.OnEnter();
    }

    protected override void OnExit()
    {
        base.OnExit();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (!_data.IsAttack)
        {
            _children[0].Execute();
            NodeResult = Result.FAILURE;
        }
        else
        {
            _children[1].Execute();
            NodeResult = Result.SUCCESS;
            UpdateState = _children[1].UpdateState;
        }
    }
}
