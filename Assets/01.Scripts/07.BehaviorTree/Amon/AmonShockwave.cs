using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Yields;
using DG.Tweening;

public class AmonShockwave : BT_Node
{
    private const string JUMP_ATTACK = "attack_03";
    private AmonData _data;
    public AmonShockwave(BehaviorTree t, List<BT_Node> c = null) : base(t, c)
    {
        _data = _tree.GetData<AmonData>();
    }
    private float _timer;
    private Vector3 _startPos;

    private Vector3 _targetPos;
    public override Result Execute()
    {
        return base.Execute();
    }


    protected override void OnEnter()
    {
        _tree.StartCoroutine(PlayAnimation());
        _tree.IsStop = true;
        _targetPos = _data.Target.position;

        Vector3 lookPos = _targetPos - _tree.transform.position;
        if(lookPos != Vector3.zero)
            _tree.transform.rotation = Quaternion.LookRotation(lookPos);
        
    }

    protected override void OnUpdate()
    {
        _timer += Time.deltaTime;
        _tree.transform.DOMoveY(_data.JumpPosY, _data.AnimatorCtrl.GetAnimationLength(JUMP_ATTACK) * 0.5f).SetLoops(2,LoopType.Yoyo);
        if(_timer >= _data.AnimatorCtrl.GetAnimationLength(JUMP_ATTACK))
        {
            _tree.transform.position = Vector3.Lerp(_startPos, _targetPos, _timer / _data.AnimatorCtrl.GetAnimationLength(JUMP_ATTACK));
        }
        else
        {
            UpdateState = UpdateState.Exit;
        }
    }

    protected override void OnExit()
    {
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.JUMP_ATTACK_End);
        base.OnExit();
    }

    private IEnumerator PlayAnimation()
    {
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.JUMP_ATTACK_START);
        yield return WaitForSeconds(_data.JumpAttackStartDelay);
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.JUMP_ATTACK);
        base.OnEnter();
        _tree.IsStop = false;
        _startPos = _tree.transform.position;
        _targetPos = _data.Target.position;
    }
}

public partial class AmonData
{
    [SerializeField]
    private float _jumpAttackStartDelay = 1f;
    public float JumpAttackStartDelay => _jumpAttackStartDelay;

    [SerializeField]
    private float _jumpPosY = 3f;
    public float JumpPosY => _jumpPosY;

}
