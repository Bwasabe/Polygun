using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmonAttackCondition : BT_Condition
{
    public AmonAttackCondition(BehaviorTree t, List<BT_Node> c) : base(t, c)
    {
    }

    public override Result Execute()
    {
        return base.Execute();
    }

    protected override void OnEnter()
    {
        base.OnEnter();
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

public partial class AmonData
{
    [SerializeField]
    private float _attackDistance;
    public float AttackDistance => _attackDistance;

}
