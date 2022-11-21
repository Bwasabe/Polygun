using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Euclades_Blackhole : BT_Node
{
    public Euclades_Blackhole(BehaviorTree t, List<BT_Node> c = null) : base(t, c)
    {
    }

    // public override Result Execute()
    // {
    //     // TODO: 블랙홀 패턴
    //     // 바닥에 블랙홀처럼 생겨 주욱 늘어난 후
    //     // 위에서 블럭이 떨어져 플레이어는 그 블럭들을 밟으면서 위로 올라가
    //     // 살아 남아야 한다
    //     return Result.FAILURE;
    // }

}
