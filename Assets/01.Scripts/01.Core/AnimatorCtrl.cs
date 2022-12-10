using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Define;


[RequireComponent(typeof(Animator))]
public class AnimatorCtrl<T>  : MonoBehaviour where T : Enum
{
    private readonly int STATE_HASH = Animator.StringToHash("State");

    private Animator _animator;

    // private T _currentState;
    // public T CurrentState {
    //     get => _currentState;
    //     set => value = _currentState;
    // }


    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimation(T animationState)
    {
        _animator.SetInteger(STATE_HASH, animationState.GetValue<int>());
    }
}
