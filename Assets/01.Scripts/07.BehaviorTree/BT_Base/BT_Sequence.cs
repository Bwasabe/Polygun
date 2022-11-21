using System.Collections.Generic;

public class BT_Sequence : BT_Node
{
    public BT_Sequence(BehaviorTree t, List<BT_Node> children) : base(t, children){}

    private int _childIndex;


    protected override void OnEnter()
    {
        base.OnEnter();
    }

    protected override void OnExit()
    {
        base.OnExit();
    }

    protected override void OnUpdate()
    {
        if(_childIndex >=_children.Count)
        {
            UpdateState = UpdateState.Exit;
            return;
        }
        Result result = _children[_childIndex].Execute();
        switch (result)
        {
            case Result.SUCCESS:
                _childIndex++;
                break;
                case Result.FAILURE:
                _childIndex = 0;
                break;
                case Result.RUNNING:
                break;
            default:
                UnityEngine.Debug.LogError("Wrong NodeResult");
                break;
        }
        NodeResult = result;
    }
    
    public override Result Execute()
    {
        return NodeResult;
        // bool isAnyNodeRunning = false;
        // foreach (var node in _children)
        // {
        //     switch (node.Execute())
        //     {
        //         case Result.RUNNING:
        //             isAnyNodeRunning = true;
        //             break;
        //         case Result.SUCCESS:
        //             break;
        //         case Result.FAILURE:
        //             NodeResult = Result.FAILURE;
        //             return NodeResult;
        //         default:
        //             break;
        //     }
        // }
        // NodeResult = isAnyNodeRunning ? Result.RUNNING : Result.SUCCESS;
        // return NodeResult;
    }
}
