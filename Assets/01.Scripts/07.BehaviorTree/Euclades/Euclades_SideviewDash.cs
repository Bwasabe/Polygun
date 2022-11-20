using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Euclades_SideviewDash : BT_Node
{
    public Euclades_SideviewDash(BehaviorTree t) : base(t)
    {
        _data = _tree.GetData<Euclades_Data>();
    }
    private Euclades_Data _data;

    public override Result Execute()
    {
        // IsViewChange를 true하고 시점이 바뀐 후에 IsViewChange = false;
        _tree.IsStop = true;
        return Result.FAILURE;
    }
}

public partial class Euclades_Data
{
    public bool IsViewChanging{ get; set; } = false;
}
