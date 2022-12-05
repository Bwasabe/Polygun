using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongEnemy : BehaviorTree
{
    [SerializeField]
    private LongEnemyData _data;

    public bool IsAttack = false;
	private GameObject _target;
    protected override BT_Node SetupTree()
    {
        _root = new BT_Selector(this, new List<BT_Node>
		{
			new LongEnemyMoveCondition(this, 
                new List<BT_Node>{
                    new LongEnemyMove(this, _target.transform, _data)
                }),
			new LongEnemyAttackCondition(this, 
                new List<BT_Node>{new LongEnmyAttack(this, _target.transform, _data)
                })
		});

        return _root;
	}

    protected override void Start()
    {
		_target = GameObject.Find("Player");
		_data.Stat.Init();
        base.Start();
    }
}
