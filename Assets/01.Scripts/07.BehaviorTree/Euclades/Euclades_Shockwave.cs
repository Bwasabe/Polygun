using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: 쇼크웨이브
public class Euclades_Shockwave : BT_Node
{
    public Euclades_Shockwave(BehaviorTree t , List<BT_Node> c = null) : base(t, c) {}

    private bool _isUp;

    protected override void OnEnter()
    {
        base.OnEnter();
        _isUp = true;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if(_isUp)
        {
            
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
    // public override Result Execute()
    // {
    //     // 트윈이 끝나면 IsStop false, Random도 초기화
    //     _tree.IsStop = true;

    //     return Result.RUNNING;
    // }
}

public partial class Euclades_Data
{
    [SerializeField]
    private float _upDuration;

    public float UpDuration => _upDuration;

    [SerializeField]
    private GameObject _shockwaveObject;

    public GameObject ShockwaveObject => _shockwaveObject;
}
