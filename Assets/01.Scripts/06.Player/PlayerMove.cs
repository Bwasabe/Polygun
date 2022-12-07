using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController))]
public class PlayerMove : BasePlayerComponent
{
    [SerializeField]
    private Vector3 _worldDir = Vector3.up;

    [SerializeField]
    private float _moveSmooth = 8f;

    private Vector3 _dir;

    private PlayerStat _playerStat;

    public bool IsFreeze { get; set; } = false;

    private CharacterController _cc;
    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    protected override void Start()
    {
        base.Start();
        _playerStat = _player.PlayerStat;
    }

    protected override void RegisterInput()
    {
        _input.AddInput("LEFT", KeyCode.A);
        _input.AddInput("RIGHT", KeyCode.D);
        _input.AddInput("FORWARD", KeyCode.W);
        _input.AddInput("BACKWARD", KeyCode.S);

    }

    private void Update()
    {

        if (IsFreeze) return;
        Vector3 moveInput = Vector3.zero;

        SetInput(ref moveInput);

        Move(moveInput);
    }

    private void SetInput(ref Vector3 input)
    {
        if (Input.GetKey(_input.GetInput("LEFT")))
        {
            input += Vector3.left;
        }
        if (Input.GetKey(_input.GetInput("RIGHT")))
        {
            input += Vector3.right;
        }
        if (Input.GetKey(_input.GetInput("FORWARD")))
        {
            input += Vector3.forward;
        }
        if (Input.GetKey(_input.GetInput("BACKWARD")))
        {
            input += Vector3.back;
        }
    }


    private void SetState(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            _player.CurrentState |= PLAYER_STATE.MOVE;
			_player.CurrentState &= ~PLAYER_STATE.IDLE;

		}
		else
        {
			_player.CurrentState |= PLAYER_STATE.IDLE;

			_player.CurrentState &= ~PLAYER_STATE.MOVE;
        }
    }

    private void Move(Vector3 input)
    {
        SetState(input);

        Vector3 forward = Define.MainCam.transform.forward;
        forward.y = 0f;

        Vector3 right = new Vector3(forward.z, 0f, -forward.x);
        _dir = (right * input.x + forward * input.z).normalized;
        _dir = Vector3.Lerp(_dir, (right * input.x + forward * input.z).normalized, Time.deltaTime * _moveSmooth);

        if (_dir != Vector3.zero && !_player.CurrentState.HasFlag(PLAYER_STATE.ATTACK))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dir), Time.deltaTime * _moveSmooth * GameManager.PlayerTimeScale);
        }
        _cc.Move(_playerStat.Speed * _dir * Time.deltaTime * GameManager.TimeScale * GameManager.PlayerTimeScale);
    }

}
