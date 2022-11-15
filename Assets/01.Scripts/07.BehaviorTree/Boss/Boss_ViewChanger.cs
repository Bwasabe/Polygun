using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_ViewChanger : BT_Node
{
    public Boss_ViewChanger(BehaviorTree t, Viewpoint point) : base(t)
    {
        _viewPoint = point;
    }

    private Viewpoint _viewPoint;
    private Result _result;

    public override Result Execute()
    {
        _tree.IsStop = true;

        _tree.StartCoroutine(ViewChanger(_viewPoint));
        return _result;
    }

    private IEnumerator ViewChanger(Viewpoint point)
    {
        Vector3 viewVec = Vector3.zero;
        viewVec = point switch
        {
            Viewpoint.BackView => Vector3.back,
            Viewpoint.SideView => Vector3.right,
            _ => Vector3.zero
        };
        //TODO: Vector에 따라 카메라 위치 변경해주기
        yield return null;
    }
    
}
