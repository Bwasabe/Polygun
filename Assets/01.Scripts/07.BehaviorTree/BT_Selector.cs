using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Selector : BT_Node
{
    protected List<BT_Node> _children;
    public BT_Selector(BehaviorTree t, List<BT_Node> children) : base(t) {
        _children = children;
    }

    public override Result Execute()
    {
        foreach (BT_Node node in _children)
        {
            switch (node.Execute())
            {
                case Result.FAILURE:
                    continue;
                case Result.SUCCESS:
                    _state = Result.SUCCESS;
                    return _state;
                case Result.RUNNING:
                    _state = Result.RUNNING;
                    return _state;
                default:
                    continue;
            }
        }

        _state = Result.FAILURE;
        return _state;
    }

}
