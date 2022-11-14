using System.Collections.Generic;

public class BT_RandomNode : BT_MultipleNode
{
    public BT_RandomNode(BehaviorTree t, int min, int max, List<BT_Node> children) : base(t, children)
    {
        _max = max;
        _min = min;
        ResetRandom();
    }

    private int _max;
    private int _min;
    protected int _random;

    public override Result Execute()
    {
        return _children[_random].Execute();
    }
    public void ResetRandom()
    {
        _random = UnityEngine.Random.Range(_min, _max);
    }
}
