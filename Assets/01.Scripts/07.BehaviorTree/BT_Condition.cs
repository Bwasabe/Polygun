using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BT_Condition : BT_Node
{
    protected BT_Node _child;

    public BT_Condition(BehaviorTree tree, BT_Node child) : base(tree)
    {
        _child = child;
    }

}
