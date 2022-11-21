using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Euclades_TopdownShooter : BT_Node
{
    public Euclades_TopdownShooter(BehaviorTree t, List<BT_Node> c= null) : base(t, c)
    {
    }

    // public override Result Execute()
    // {
    //     // TODO: TopdownBulletCount 만큼 총 사용하면서 탑다운 슈터 느낌 내주기
    //     return Result.RUNNING;
    // }
}

public partial class Euclades_Data
{
    [SerializeField]
    private int _topdownBulletCount;

    public int TopdownBulletCount => _topdownBulletCount;
}
