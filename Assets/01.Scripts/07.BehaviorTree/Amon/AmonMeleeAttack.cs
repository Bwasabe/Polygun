using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Yields;

public class AmonMeleeAttack : BT_Node
{
    private AmonData _data;

    private const string MELEE_ATTACK = "attack_01";
    public AmonMeleeAttack(BehaviorTree t, List<BT_Node> c = null) : base(t, c)
    {
        _data = _tree.GetData<AmonData>();
    }

    public override Result Execute()
    {
        return base.Execute();
    }

    protected override void OnEnter()
    {
        NodeResult = Result.RUNNING;
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.MELEE_ATTACK);
        _tree.IsStop = true;
        _tree.StartCoroutine(StopAnimation());
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    protected override void OnExit()
    {
        base.OnExit();
    }

    private IEnumerator StopAnimation()
    {
        yield return WaitForSeconds(_data.AnimatorCtrl.GetAnimationLength(MELEE_ATTACK));
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.IDLE);
        _tree.IsStop = false;
        NodeResult = Result.SUCCESS;
        UpdateState = UpdateState.None;
    }
}

public partial class AmonData
{
    [SerializeField]
    private float _attackDelay = 1f;
    public float AttackDelay => _attackDelay;
}
