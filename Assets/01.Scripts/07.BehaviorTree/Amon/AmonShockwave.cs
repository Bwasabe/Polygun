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


    // private bool _isJumping;
    public override Result Execute()
    {
        return base.Execute();
    }


    protected override void OnEnter()
    {
        _data.IsShockwave = true;
        _tree.IsStop = true;
        _tree.StartCoroutine(PlayAnimation());

        _targetPos = _data.Target.position;

        Vector3 lookPos = _targetPos - _tree.transform.position;
        if (lookPos != Vector3.zero)
        {
            _tree.transform.rotation = Quaternion.Slerp(_tree.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * _data.RotateSmooth);
            _tree.transform.rotation = Quaternion.Euler(0f, _tree.transform.eulerAngles.y, 0f);
        }

    }

    protected override void OnUpdate()
    {
        // _tree.transform.DOMoveY(_data.JumpPosY, _data.AnimatorCtrl.GetAnimationLength(JUMP_ATTACK) * 0.4f).SetLoops(2, LoopType.Yoyo);
        // Debug.Log("업데이트");

        _timer += Time.deltaTime;
        if (_timer <= _data.AnimatorCtrl.GetAnimationLength(JUMP_ATTACK))
        {
            _tree.transform.position = Vector3.Lerp(_startPos, _targetPos, _timer / _data.AnimatorCtrl.GetAnimationLength(JUMP_ATTACK));
        }
        else
        {
            // TODO: 쇼크 웨이브 소환
            // TODO: 카메라 쉐이크
            CameraManager.Instance.CameraShake();
            // Debug.Break();
            _data.CC.enabled = true;
            _timer = 0f;
            _tree.IsStop = true;
            _tree.StartCoroutine(Shockwave());


        }
    }

    private IEnumerator Shockwave()
    {
        for (int i = 1; i <= _data.ShockwaveFireCount; ++i)
        {
            int fireCount = (int)(360f / _data.ShockwaveFireAngle);
            for (int j = 0; j < fireCount; ++j)
            {
                float angle = j * _data.ShockwaveFireAngle * Mathf.Deg2Rad;

                AmonFire g = ObjectPool.Instance.GetObject(PoolObjectType.FireShockWave).GetComponent<AmonFire>();

                g.transform.position =
                    new Vector3(
                        Mathf.Cos(angle) * i + _data.ShockwaveStartDistance,
                        0f,
                        Mathf.Sin(angle) * i + _data.ShockwaveStartDistance) + _tree.transform.position;

                g.transform.rotation = _tree.transform.rotation;
                    
                g.gameObject.SetActive(true);
                g.Duration = _data.ShockwaveDuration;
            }
            yield return WaitForSeconds(_data.ShockwaveFireDelay);
        }
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.JUMP_ATTACK_End);
        _tree.StartCoroutine(EndAnimation());
    }

    protected override void OnExit()
    {
        _timer = 0f;
        base.OnExit();
        _data.IsShockwave = false;
    }

    private IEnumerator EndAnimation()
    {
        yield return WaitForSeconds(_data.AnimatorCtrl.GetAnimationLength(JUMP_ATTACK_END));
        _tree.IsStop = false;
        UpdateState = UpdateState.Exit;
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.IDLE);
    }

    private IEnumerator PlayAnimation()
    {
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.JUMP_ATTACK_START);
        yield return WaitForSeconds(_data.AnimatorCtrl.GetAnimationLength("Jump_Attack_Wait") + 1f);
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.JUMP_ATTACK);
        base.OnEnter();
        _data.CC.enabled = false;
        _tree.IsStop = false;

        _targetPos = _data.Target.position;
        _targetPos.y = _tree.transform.position.y;
        _startPos = _tree.transform.position;
        _startPos.y = Mathf.Max(_data.GroundHeight, _startPos.y);

    }
}

public partial class AmonData
{

    // [SerializeField]
    // private float _jumpPosY = 3f;
    // public float JumpPosY => _jumpPosY;

    [SerializeField]
    private float _rotateSmooth = 8f;
    public float RotateSmooth => _rotateSmooth;

    public bool IsShockwave { get; set; } = false;

    [SerializeField]
    private float _groundHeight = 1f;
    public float GroundHeight => _groundHeight;

    [SerializeField]
    private int _shockwaveFireCount = 10;
    public int ShockwaveFireCount => _shockwaveFireCount;

    [SerializeField]
    private float _shockwaveFireDelay = 0.1f;
    public float ShockwaveFireDelay => _shockwaveFireDelay;

    [SerializeField]
    private float _shockwaveFireAngle = 45f;
    public float ShockwaveFireAngle => _shockwaveFireAngle;

    [SerializeField]
    private float _shockwaveStartDistance = 1f;
    public float ShockwaveStartDistance => _shockwaveStartDistance;

    [SerializeField]
    private float _shockwaveDuration = 10f;
    public float ShockwaveDuration => _shockwaveDuration;

    [SerializeField]
    private AmonFire _shockwaveFirePrefab;
    public AmonFire ShockwaveFirePrefab => _shockwaveFirePrefab;
}
