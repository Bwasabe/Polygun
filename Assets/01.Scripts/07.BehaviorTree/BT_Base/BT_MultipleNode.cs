using System.Collections.Generic;

public class BT_MultipleNode : BT_Node
{
    protected List<BT_Node> _children;
    public BT_MultipleNode(BehaviorTree t, List<BT_Node> children) : base(t)
    {
        _children = children;
    }
}
