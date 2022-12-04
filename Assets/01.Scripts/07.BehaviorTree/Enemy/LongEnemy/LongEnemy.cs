using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongEnemy : BehaviorTree
{
    [SerializeField]
    private LongEnemyData _data;

    public bool IsAttack = false;
    protected override BT_Node SetupTree()
    {
        throw new System.NotImplementedException();
    }

    protected override void Start()
    {
        _data.Stat.Init();
        base.Start();
    }
}
