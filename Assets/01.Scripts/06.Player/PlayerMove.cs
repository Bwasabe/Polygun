using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController))]
public class PlayerMove : BasePlayerComponent
{
    [SerializeField]
    private float _playerSpeed = 8f;

    [SerializeField]
    private Vector3 _worldDir = Vector3.up;

    [SerializeField]
    private float _moveSmooth = 8f;

    private Vector3 _dir;

    public bool IsFreeze { get; set; } = false;

    private CharacterController _cc;
    private void Awake() {
        _cc = GetComponent<CharacterController>();
    }

    protected override void RegisterInput()
    {
        _input.AddInput("LEFT", KeyCode.A);
        _input.AddInput("RIGHT", KeyCode.D);
        _input.AddInput("FORWARD", KeyCode.W);
        _input.AddInput("BACKWARD", KeyCode.S);
    }

    private void Update() {

        if(IsFreeze)return;
        Vector3 moveInput = Vector3.zero;

        SetInput(ref moveInput);

        Move(moveInput);
    }

    private void SetInput(ref Vector3 input)
    {
        if (Input.GetKey(_input.InputDict["LEFT"]))
        {
            input += Vector3.left;
        }
        if (Input.GetKey(_input.InputDict["RIGHT"]))
        {
            input += Vector3.right;
        }
        if (Input.GetKey(_input.InputDict["FORWARD"]))
        {
            input += Vector3.forward;
        }
        if (Input.GetKey(_input.InputDict["BACKWARD"]))
        {
            input += Vector3.back;
        }
    }

    
    private void SetState(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            _player.CurrentState |= PLAYER_STATE.MOVE;
        }
        else
        {
            _player.CurrentState = PLAYER_STATE.IDLE;
        }
    }

    private void Move(Vector3 input)
    {
        SetState(input);

        Debug.Log(Define.MainCam.transform.forward);
        Vector3 forward = _worldDir;
        forward.y = 0f;

        Vector3 right = new Vector3(forward.z, 0f, -forward.x);
        _dir = Vector3.Lerp(_dir, (right * input.x + forward * input.z).normalized, Time.deltaTime * _moveSmooth);


        _cc.Move(_playerSpeed * _dir * Time.deltaTime * GameManager.TimeScale);
    }

}
