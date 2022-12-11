using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Yields;

public class AmonMeleeAttack : BT_Node
{
    private AmonData _data;
    private const string MELEE_ATTACK = "attack_01";

    private float _timer;
    public AmonMeleeAttack(BehaviorTree t, List<BT_Node> c = null) : base(t, c)
    {
        _data = _tree.GetData<AmonData>();
    }

    public override Result Execute()
    {
        base.Execute();
        return NodeResult;
    }

    protected override void OnEnter()
    {
        NodeResult = Result.RUNNING;
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.MELEE_ATTACK);
        base.OnEnter();
        _data.IsAttack = true;
    }

    protected override void OnUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer >= _data.AnimatorCtrl.GetAnimationLength(MELEE_ATTACK))
        {
            _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.IDLE);
            NodeResult = Result.SUCCESS;
            UpdateState = UpdateState.Exit;
            _data.IsAttack = false;
            // TODO: 불렛 소환
        }
    }

    protected override void OnExit()
    {
        _timer = 0f;
        base.OnExit();
    }
}

public partial class AmonData
{
    [SerializeField]
    private float _attackDelay = 1f;
    public float AttackDelay => _attackDelay;
}
