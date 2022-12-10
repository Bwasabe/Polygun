using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimatorCtrl<Amon_Animation_State>))]
public class Amon : BehaviorTree
{
    [SerializeField]
    private AmonData _data;

    [SerializeField]
    private Collider _attackCol;

    private Animator _animator;
    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        _data.AnimatorCtrl = new AnimatorCtrl<Amon_Animation_State>(_animator);
    }

    protected override BT_Node SetupTree()
    {
        _root = new BT_Selector(this, new List<BT_Node>()
        {
            new AmonSleep(this),
            new BT_ListRandomNode(this, Define.DEFAULT_RANDOM_NUM, Define.DEFAULT_RANDOM_NUM, new List<BT_Node>()
            {
                new BT_Selector(this, new List<BT_Node>(){
                    new AmonAttackCondition(this, new List<BT_Node>(){
                        new AmonAttack(this),
                    }),
                    new AmonFollow(this),
                }),
                new AmonShockwave(this)
            }),
        });
        return _root;
    }


    protected void EventAttackColliderStart()
    {
        _attackCol.enabled = true;
    }

    protected void EventAttackColliderEnd()
    {
        _attackCol.enabled = true;
    }
}

public enum Amon_Animation_State
{
    IDLE,
    WALK,
    RUN,
    CHARGE_ATTACK,
    SLEEP_START,
    SLEEP_END,
    JUMP,
    LAND,
    DAMAGED,
    RAGE,
    DIE
}

public partial class AmonData
{
    // TODO: 타임라인 끝나면 Transform에 플레이어 넣어주기
    public Transform Target { get; set; } = null;

    public AnimatorCtrl<Amon_Animation_State> AnimatorCtrl{ get; set; }
}