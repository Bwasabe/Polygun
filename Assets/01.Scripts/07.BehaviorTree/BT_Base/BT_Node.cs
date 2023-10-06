using System.Collections.Generic;
using System.Diagnostics;

public enum Result
{
    RUNNING,
    SUCCESS,
    FAILURE,
}

public enum UpdateState
{
    None,
    Enter,
    Update,
    Exit
}

public class BT_Node
{
    public Result NodeResult { get; set; } = Result.FAILURE;

    protected BehaviorTree _tree;
    public UpdateState UpdateState { get; set; } = UpdateState.None;

    protected List<BT_Node> _children;

    public BT_Node(BehaviorTree t, List<BT_Node> c = null)
    {
        _tree = t;
        _children = c;
    }

    public virtual Result Execute()
    {
        if(UpdateState.Equals(UpdateState.None))
        {
            EnterNode();
        }

        if (UpdateState.Equals(UpdateState.Enter))
        {
            OnEnter();
        }

        if (UpdateState.Equals(UpdateState.Update))
        {
            OnUpdate();
        }

        if (UpdateState.Equals(UpdateState.Exit))
        {
            OnExit();
        }

        return Result.FAILURE;
    }



    public void EnterNode()
    {
        UpdateState = UpdateState.Enter;
        if(_children==null)return;
        foreach (BT_Node child in _children)
        {
            if (child.UpdateState.Equals(UpdateState.None))
            {
                child.UpdateState = UpdateState.Enter;
                child.EnterNode();
            }
        }
    }

    protected virtual void OnEnter()
    {
        UpdateState = UpdateState.Update;
    }

    protected virtual void OnUpdate()
    {

    }

    protected virtual void OnExit()
    {
        UpdateState = UpdateState.None;
    }
}


