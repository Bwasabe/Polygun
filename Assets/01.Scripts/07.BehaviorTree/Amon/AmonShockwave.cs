using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Yields;
using DG.Tweening;

public class AmonShockwave : BT_Node
{
    private const string JUMP_ATTACK = "attack_03";
    private const string JUMP_ATTACK_END = "Jump_Attack_End";
    private AmonData _data;
    public AmonShockwave(BehaviorTree t, List<BT_Node> c = null) : base(t, c)
    {
        _data = _tree.GetData<AmonData>();
    }
    private float _timer;
    private Vector3 _startPos;

    private Vector3 _targetPos;

    private bool _isEndAnim = false;

    // private bool _isJumping;
    public override Result Execute()
    {
        return base.Execute();
    }


    protected override void OnEnter()
    {
        Debug.Log("엔터");

        _tree.IsStop = true;
        _tree.StartCoroutine(PlayAnimation());

        _targetPos = _data.Target.position;


        Vector3 lookPos = _targetPos - _tree.transform.position;
        if (lookPos != Vector3.zero)
        {
            _tree.transform.rotation = Quaternion.Slerp(_tree.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * _data.RotateSmooth) ;
            _tree.transform.rotation = Quaternion.Euler(0f, _tree.transform.eulerAngles.y, 0f);
        }

    }

    protected override void OnUpdate()
    {
        // _tree.transform.DOMoveY(_data.JumpPosY, _data.AnimatorCtrl.GetAnimationLength(JUMP_ATTACK) * 0.4f).SetLoops(2, LoopType.Yoyo);
        // Debug.Log("업데이트");
        if (_isEndAnim)
        {
            return;
        }
        else
        {
            _timer += Time.deltaTime;
            if (_timer <= _data.AnimatorCtrl.GetAnimationLength(JUMP_ATTACK))
            {
                _tree.transform.position = Vector3.Lerp(_startPos, _targetPos, _timer / _data.AnimatorCtrl.GetAnimationLength(JUMP_ATTACK));
            }
            else
            {
                // Debug.Break();
                _data.CC.enabled = true;
                _timer = 0f;
                _tree.IsStop = true;
                _isEndAnim = true;
                _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.JUMP_ATTACK_End);
                _tree.StartCoroutine(EndAnimation());
            }
        }
    }

    protected override void OnExit()
    {
        Debug.Log("엑시트");
        _timer = 0f;
        base.OnExit();
    }

    private IEnumerator EndAnimation()
    {
        yield return WaitForSeconds(_data.AnimatorCtrl.GetAnimationLength(JUMP_ATTACK_END));
        _isEndAnim = false;
        _tree.IsStop = false;
        UpdateState = UpdateState.Exit;
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.IDLE);
    }

    private IEnumerator PlayAnimation()
    {
        // yield return WaitUntil(() => _data.AnimatorCtrl.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Equals("Idle"));
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.JUMP_ATTACK_START);
        yield return WaitForSeconds(_data.AnimatorCtrl.GetAnimationLength("Jump_Attack_Wait") + 1f);
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.JUMP_ATTACK);
        base.OnEnter();
        _data.CC.enabled = false;
        _tree.IsStop = false;
        
        _targetPos = _data.Target.position;
        _startPos = _tree.transform.position;
        // _targetPos = _data.Target.position;
    }
}

public partial class AmonData
{

    [SerializeField]
    private float _jumpPosY = 3f;
    public float JumpPosY => _jumpPosY;

    [SerializeField]
    private float _rotateSmooth = 8f;
    public float RotateSmooth => _rotateSmooth;
}
