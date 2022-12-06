using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class BoomEnemy : BehaviorTree
{
    [SerializeField]
    private BoomEnemyData boom_data;
    public UnitStat _stat => boom_data.Stat;

	public Action endAnimation;
	protected override BT_Node SetupTree()
    {
        _root = new BT_Selector(this, new List<BT_Node>
        {
            new BoomEnemyMoveCondition(this, new List<BT_Node>{new BoomEnemyMove(this)}),
            new BoomEnemyAttackCondition(this, new List<BT_Node>{new BoomEnemyAttack(this)})
		});
        return _root;
    }

	public void ExecuteAction()
	{
		endAnimation.Invoke();
	}
}
