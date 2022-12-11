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
        UpdateState = _children[0].UpdateState;
        if(Vector3.Distance(_data.Target.position, _tree.transform.position) <= _data.AttackDistance)
        {
            _children[0].Execute();
            return Result.SUCCESS;
        }
        else
        {
            _data.IsFollow = true;
            return Result.FAILURE;
        }
    }
}

public partial class AmonData
{
    [SerializeField]
    private float _attackDistance;
    public float AttackDistance => _attackDistance;

}
