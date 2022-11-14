using System.Collections.Generic;

public class BT_Sequence : BT_MultipleNode
{
    public BT_Sequence(BehaviorTree t, List<BT_Node> children) : base(t, children){}
    
    public override Result Execute()
    {
        bool isAnyNodeRunning = false;
        foreach (var node in _children)
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
