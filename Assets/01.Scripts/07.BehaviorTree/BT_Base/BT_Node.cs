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

}


