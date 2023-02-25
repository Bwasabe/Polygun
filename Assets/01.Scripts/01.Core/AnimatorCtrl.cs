using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class AnimatorCtrl<T> where T : Enum
{
    private readonly int STATE_HASH = Animator.StringToHash("State");

    private Dictionary<string, AnimationClip> _animationClipDict = new Dictionary<string, AnimationClip>();

    private Animator _animator;

    // public Animator Animator => _animator;

    private T _animationState;

    public AnimatorCtrl(Animator animator)
    {
        _animator = animator;

        for (int i = 0; i < _animator.runtimeAnimatorController.animationClips.Length; ++i)
        {
            AnimationClip clip = _animator.runtimeAnimatorController.animationClips[i];
            _animationClipDict.Add(clip.name, clip);
        }
    }

    public float GetAnimationLength(string name)
    {
        if(_animationClipDict.TryGetValue(name, out AnimationClip value))
        {
            return value.length;
        }
        else
        {
            throw new System.Exception("Name is Wrong or None in Dictionary");
        }
    }

    public void SetAnimationState(T animationState)
    {
        _animationState = animationState;
        _animator.SetInteger(STATE_HASH, animationState.GetEnumValue<int>());
    }

    public void SetAnimationStateOnce(T animationState)
    {
        if(_animationState.Equals(animationState))
            SetAnimationState(animationState);
    }
}
