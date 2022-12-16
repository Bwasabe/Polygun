using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_ListRandomNode : BT_Node
{
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
        _random = Random.Range(_min, _max);

        ResetList();
    }

    private int _max;
    private int _min;
    private int _random;


    private List<BT_Node> _currentChildren;

    protected override void OnEnter()
    {
        //Debug.Log("리스트 엔터");
        base.OnEnter();
    }

    protected override void OnExit()
    {
        _currentChildren.RemoveAt(_random);
        if (_currentChildren.Count <= 0)
        {
            ResetList();
        }
        _random = Random.Range(0, _currentChildren.Count);
        base.OnExit();
    }

    protected override void OnUpdate()
    {
        NodeResult = _currentChildren[_random].Execute();
        UpdateState = _currentChildren[_random].UpdateState;
        if (UpdateState == UpdateState.None)
        {
            UpdateState = UpdateState.Exit;
        }
    }

    public override Result Execute()
    {
        base.Execute();
        return NodeResult;
    }

    public void ResetList()
    {
        _currentChildren = new List<BT_Node>(_children);
    }


}
