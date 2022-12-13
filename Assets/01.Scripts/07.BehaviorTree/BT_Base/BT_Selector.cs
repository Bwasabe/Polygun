using System.Collections.Generic;

public class BT_Selector : BT_Node
{
    public BT_Selector(BehaviorTree t, List<BT_Node> c) : base(t, c) { }


    protected override void OnEnter()
    {
        UnityEngine.Debug.Log("들어옴");
        base.OnEnter();
    }

    protected override void OnUpdate()
    {
        foreach (BT_Node child in _children)
        {
            Result result = child.Execute();
            UnityEngine.Debug.Log($"{result}: {child.GetType()}");
            switch (result)
            {
                case Result.FAILURE:
                    continue;
                case Result.RUNNING:
                    NodeResult = Result.RUNNING;
                    continue;
                case Result.SUCCESS:
                    NodeResult = Result.SUCCESS;
                    return;
                default:
                    continue;
            }
        }
        if(IsExit())
        {
            UnityEngine.Debug.Log("나감");
            UpdateState = UpdateState.Exit;
        }
    }

    private bool IsExit()
    {
        for (int i = 0; i < _children.Count; ++i)
        {
            UnityEngine.Debug.Log($"{_children[i].GetType()} / {_children[i].UpdateState}");
            if(_children[i].UpdateState != UpdateState.None)
            {
                return false;
            }
        }
        return true;
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
