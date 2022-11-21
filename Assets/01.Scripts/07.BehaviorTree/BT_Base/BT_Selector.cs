using System.Collections.Generic;

public class BT_Selector : BT_Node
{
    public BT_Selector(BehaviorTree t, List<BT_Node> c) : base(t, c){}

    protected override void OnEnter()
    {
        base.OnEnter();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        foreach (BT_Node child in _children)
        {
            switch (child.Execute())
            {
                case Result.FAILURE:
                    continue;
                case Result.RUNNING:
                    NodeResult = Result.RUNNING;
                    continue;
                case Result.SUCCESS:
                    NodeResult = Result.SUCCESS;
                    UpdateState = UpdateState.Exit;
                    break;
                default:
                    continue;
            }
        }
    }

    protected override void OnExit()
    {
        base.OnExit();
    }

    public override Result Execute()
    {
        base.Execute();

        return NodeResult;
    }

}
