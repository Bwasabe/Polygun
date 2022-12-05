using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class MeleeFollow : BT_Node
{
    private MeleeEnemy_Data _data;
    private Transform _player;
    public MeleeFollow(BehaviorTree t,  Transform player,List<BT_Node> c = null) : base(t, c)
    {
        _player = player;
		_data = _tree.GetData<MeleeEnemy_Data>();
	}

    protected override void OnUpdate()
    {
        _tree.transform.position = Vector3.Lerp(_tree.transform.position, _player.transform.position, Time.deltaTime  * _data.Stat.Speed);
		_tree.transform.LookAt(_player);
		_data.Animator.SetBool("IsWalk", true);
        NodeResult = Result.RUNNING;
		if (Vector3.Distance(_tree.transform.position, _player.position) <= _data.attackRange)
		{
			UpdateState = UpdateState.Exit;
			NodeResult = Result.SUCCESS;
		}
	}

    protected override void OnExit()
    {
		_data.Animator.SetBool("IsWalk", false);
        base.OnExit();
	}

    public override Result Execute()
    {
        return base.Execute();
    }
}
