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

        Vector3 player = _player.transform.position;
        player.y = _tree.transform.position.y;
		_tree.transform.position = Vector3.Lerp(_tree.transform.position, player, Time.deltaTime  * _data.Stat.Speed);
		_tree.transform.LookAt(player);

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
