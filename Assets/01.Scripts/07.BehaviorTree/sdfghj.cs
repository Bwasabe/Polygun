using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sdfghj : BehaviorTree
{
    [SerializeField]
    private BT_BossData _asd = new BT_BossData();

    protected override BT_Node SetupTree()
    {
        Debug.Log("ㅁㄴㄹㅇ");
        return new BT_Selector(this, null);
    }
}
