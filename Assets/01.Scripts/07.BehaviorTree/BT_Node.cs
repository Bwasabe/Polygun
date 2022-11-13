using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Result
{
    RUNNING,
    SUCCESS,
    FAILURE,
}

public abstract class BT_Node
{
    protected Result _state;

    protected BehaviorTree _tree;

    public BT_Node(BehaviorTree t)
    {
        _tree = t;
    }

    public abstract Result Execute();

    // public Node(BehaviorTree t, List<Node> children)
    // {
    //     _tree = t;
    //     children.ForEach(x => Attatch(x));
    // }
    // private void Attatch(Node node)
    // {
    //     node._parent = this;
    //     _children.Add(node);
    // }


}


