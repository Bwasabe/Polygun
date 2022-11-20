using System.Collections.Generic;

public class BT_RandomNode : BT_MultipleNode
{
    public BT_RandomNode(BehaviorTree t, int min, int max, List<BT_Node> children) : base(t, children)
    {
        if(max.Equals(Define.DEFAULT_RANDOM_NUM))
        {
            _max = children.Count;
        }
        else
        {
            _max = max;
        }
        if(min.Equals(Define.DEFAULT_RANDOM_NUM))
        {
            _min = 0;
        }
        else
        {
            _min = min;
        }
        ResetRandom();
    }

    private int _max;
    private int _min;
    private int _random;

    public override Result Execute()
    {
        return _children[_random].Execute();
    }
    public void ResetRandom()
    {
        _random = UnityEngine.Random.Range(_min, _max);
    }
}
