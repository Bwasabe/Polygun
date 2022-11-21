    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_ListRandomNode : BT_Node
{

    // public BT_ListRandomNode(BehaviorTree t, int min, int max, List<BT_Node> children) : base(t, children)
    // {
    //     if (min.Equals(Define.DEFAULT_RANDOM_NUM))
    //     {
    //         _min = 0;
    //     }
    //     else
    //     {
    //         _min = min;
    //     }
    //     if (max.Equals(Define.DEFAULT_RANDOM_NUM))
    //     {
    //         _max = children.Count;
    //     }
    //     else
    //     {
    //         _max = max;
    //     }

    //     _tempChildren = children;
    // }


    public BT_ListRandomNode(BehaviorTree t, int min, int max, List<BT_Node> c) : base(t, c)
    {
        if (min.Equals(Define.DEFAULT_RANDOM_NUM))
        {
            _min = 0;
        }
        else
        {
            _min = min;
        }
        if (max.Equals(Define.DEFAULT_RANDOM_NUM))
        {
            _max = c.Count;
        }
        else
        {
            _max = max;
        }

        _tempChildren = c;
    }

    private int _max;
    private int _min;
    private int _random;

    private List<BT_Node> _tempChildren = new List<BT_Node>();

    private List<BT_Node> _currentChildren;

    protected override void OnEnter()
    {
        base.OnEnter();
        _random = Random.Range(_min, _max);
    }

    protected override void OnExit()
    {
        _currentChildren.RemoveAt(_random);
        base.OnExit();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override Result Execute()
    {
        return _children[_random].Execute();
    }

    // public override BT_Node Execute()
    // {
    //     if (_currentChildren.Count <= 0)
    //     {
    //         ResetList();
    //     }

    //     int random = Random.Range(_min, _max);

    //     Result result = _currentChildren[random].Execute();

    //     _currentChildren.RemoveAt(random);

    //     return result;

    // }

    public void ResetList()
    {
        _currentChildren = new List<BT_Node>(_tempChildren);

    }


}
