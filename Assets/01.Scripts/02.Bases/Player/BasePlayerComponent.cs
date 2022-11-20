using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerComponent : MonoBehaviour
{
    protected Player _player;

    protected InputManager _input;

    protected virtual void Start()
    {
        _player = GameManager.Instance.Player;
        _input = GameManager.Instance.InputManager;
        
        RegisterInput();
    }

    /// <summary>
    /// 키 세팅을 위해 키 등록 해두는 함수
    /// </summary>
    protected virtual void RegisterInput() { }
}
