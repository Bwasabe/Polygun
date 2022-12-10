using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmonAttack : BT_Node
{
    private AmonData _data;

    public AmonAttack(BehaviorTree t, List<BT_Node> c = null) : base(t, c)
    {
        _data = _tree.GetData<AmonData>();
    }

    public override Result Execute()
    {
        return base.Execute();
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        NodeResult = Result.RUNNING;
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.CHARGE_ATTACK);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }
    protected override void OnExit()
    {
        base.OnExit();
    }

}
