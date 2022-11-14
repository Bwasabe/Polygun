public enum Result
{
    RUNNING,
    SUCCESS,
    FAILURE,
}

public class BT_Node
{
    protected Result _state;

    protected BehaviorTree _tree;

    public BT_Node(BehaviorTree t)
    {
        _tree = t;
    }

    public virtual Result Execute()
    {
        return Result.FAILURE;
    }

}


