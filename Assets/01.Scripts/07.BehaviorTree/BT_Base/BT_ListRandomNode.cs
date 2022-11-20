using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_ListRandomNode : BT_MultipleNode
{

    public BT_ListRandomNode(BehaviorTree t, int min, int max, List<BT_Node> children) : base(t, children)
    {
        if (min.Equals(BT_RandomNode.Define.DEFAULT_RANDOM_NUM))
        {
            _min = 0;
        }
        else
        {
            _min = min;
        }
        if (max.Equals(BT_RandomNode.Define.DEFAULT_RANDOM_NUM))
        {
            _max = children.Count;
        }
        else
        {
            _max = max;
        }

        _tempChildren = children;
    }

    private int _max;
    private int _min;

    private List<BT_Node> _tempChildren = new List<BT_Node>();

    private List<BT_Node> _currentChildren;

    public override Result Execute()
    {
        if (_currentChildren.Count <= 0)
        {
            ResetList();
        }

        int random = Random.Range(_min, _max);

        Result result = _currentChildren[random].Execute();

        _currentChildren.RemoveAt(random);

        return result;

    }

    public void ResetList()
    {
        _currentChildren = new List<BT_Node>(_tempChildren);

    }


}
