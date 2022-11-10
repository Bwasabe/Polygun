using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Sequence : BT_Node
{
    protected List<BT_Node> nodes;

    public BT_Sequence(BehaviorTree t, List<BT_Node> nodes) : base(t)
    {
        this.nodes = nodes;
    }
    public override Result Execute()
    {
        bool isAnyNodeRunning = false;
        foreach (var node in nodes)
        {
            switch (node.Execute())
            {
                case Result.RUNNING:
                    isAnyNodeRunning = true;
                    break;
                case Result.SUCCESS:
                    break;
                case Result.FAILURE:
                    _state = Result.FAILURE;
                    return _state;
                default:
                    break;
            }
        }
        _state = isAnyNodeRunning ? Result.RUNNING : Result.SUCCESS;
        return _state;
    }
}
