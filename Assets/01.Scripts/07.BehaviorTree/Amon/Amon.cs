using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amon : BehaviorTree
{

    protected override BT_Node SetupTree()
    {
        _root = new BT_Selector(this, new List<BT_Node>()
        {
            new AmonSleep(this),
            new BT_ListRandomNode(this, Define.DEFAULT_RANDOM_NUM, Define.DEFAULT_RANDOM_NUM, new List<BT_Node>()
            {
                new BT_Selector(this, new List<BT_Node>(){
                    new AmonAttackCondition(this, new List<BT_Node>(){
                        new AmonAttack(this),
                    }),
                    new AmonFollow(this),
                }),
                new AmonShockwave(this)
            }),
        });
        return _root;
    }

}
