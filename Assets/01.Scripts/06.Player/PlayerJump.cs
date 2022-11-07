using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerJump : BasePlayerComponent
{
    public enum JUMP_STATE
    {
        NONE = 0,
        JUMPUP = 1,
        JUMPDOWN = 2,
    }

    private JUMP_STATE _jumpState = JUMP_STATE.NONE;


    [SerializeField]
    private float _jumpForce = 3f;
    [SerializeField]
    private float _gravityScale = 3f;

    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private Vector3 _velocity;

    private CharacterController _cc;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    protected override void Start()
    {
        base.Start();
        OnGUIManager.Instance._guiDict.Add("IsGround", "");
    }

    protected override void RegisterInput()
    {
        _input.AddInput("JUMP", KeyCode.Space);
    }

    private void Update()
    {
        Jump();
    }

    private void LateUpdate()
    {
        AdjoinGround();
    }

    private void CheckJumpState()
    {
        if (_velocity.y > 0)
        {
            _jumpState = JUMP_STATE.JUMPUP;
        }
        else
        {
            _jumpState = JUMP_STATE.JUMPDOWN;
        }
    }

    private void Jump()
    {
        CheckJumpState();
        OnGUIManager.Instance._guiDict["IsGround"] = $"{_cc.collisionFlags}";
        if (IsGround())
        {
            if (_jumpState == JUMP_STATE.JUMPDOWN)
            {
                _velocity.y = 0f;
                _jumpState = JUMP_STATE.NONE;
                _player.CurrentState &= ~PLAYER_STATE.JUMP;
            }
            if (Input.GetKey(_input.GetInput("JUMP"))
            && _jumpState == JUMP_STATE.NONE && !_player.CurrentState.HasFlag(PLAYER_STATE.JUMP))
            {
                _velocity.y = Mathf.Sqrt(_jumpForce * -2.0f * Physics.gravity.y) * GameManager.TimeScale;
                Debug.Log("점프 눌림");
                _player.CurrentState |= PLAYER_STATE.JUMP;
            }
        }
        _velocity.y += Physics.gravity.y * Time.deltaTime * _gravityScale * GameManager.TimeScale;

        _cc.Move(_velocity * Time.deltaTime * GameManager.TimeScale);
    }

    public bool IsGround()
    {
        Vector3 pos2 = transform.position + _cc.center;
        float value = _cc.height * 0.5f - _cc.radius;

        pos2.y -= value + _cc.skinWidth + 0.1f;

        return Physics.CheckSphere(pos2, _cc.radius, _groundLayer);
    }

    private void OnDrawGizmos()
    {
        try
        {
            Gizmos.color = Color.red;
            Vector3 pos2 = transform.position + _cc.center;
            float value = _cc.height * 0.5f - _cc.radius;

            pos2.y -= value + _cc.skinWidth + 0.1f;
            Gizmos.DrawSphere(pos2, _cc.radius);
        }
        catch { }

    }

    /// <summary>
    /// 플레이어가 내리막길 같은 곳을 갈 때 땅에 붙어서 갈 수 있도록 해주는 함수
    /// </summary>
    private void AdjoinGround()
    {
        OnGUIManager.Instance._guiDict["JUMPSTATE"] = $"JumpState : {_jumpState}";
        Debug.Log(IsGround());
        if (_player.CurrentState.HasFlag(PLAYER_STATE.JUMP)) return;

        if (!(_jumpState == JUMP_STATE.NONE)) return;

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, _groundLayer))
        {
            Vector3 pos2 = transform.position + _cc.center;
            float value = _cc.height * 0.5f;

            pos2.y -= value + _cc.skinWidth + 0.001f;

            float distance = pos2.y - hit.point.y;

            _cc.Move(Vector3.down * distance);

        }
    }

}
