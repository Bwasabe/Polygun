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
        _animator = GetComponentInChildren<Animator>();
        _data.AnimatorCtrl = new AnimatorCtrl<Amon_Animation_State>(_animator);
        _data.CC = GetComponent<CharacterController>();
    }

    protected override void Start() {
        _data.Target = GameManager.Instance.Player.transform;
        base.Start();

    }

    protected override void Update()
    {
        base.Update();
    }

    protected override BT_Node SetupTree()
    {
        _root = new BT_ListRandomNode(this, Define.DEFAULT_RANDOM_NUM, Define.DEFAULT_RANDOM_NUM, new List<BT_Node>()
            {
                new AmonAttackCondition(this, new List<BT_Node>(){
                    new AmonFollow(this),
                    new AmonMeleeAttack(this),
                }),
                new AmonProjectileAttack(this),
                new AmonShockwave(this)
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

    protected void EventProjectileAttack()
    {
        Bullet bullet = Instantiate(_data.AmonProjectileBullet, _data.AmonProjectileAttackPos.position, Quaternion.identity);
        GameObject particle = Instantiate(_data.AmonProjectileParticle, _data.AmonProjectileAttackPos.position, Quaternion.identity);

        ParticleSystem pivot = particle.transform.Find("Pivot").GetComponent<ParticleSystem>();
        // pivot.main.startSpeed = _data.ProjectileBulletSpeed;

    }
}

public enum Amon_Animation_State
{
    IDLE = 0,
    WALK = 1,
    RUN = 2,
    MELEE_ATTACK = 3,
    PROJECTILE_ATTACK = 4,
    JUMP_ATTACK_START = 5,
    JUMP_ATTACK = 6,
    JUMP_ATTACK_End = 7,
    JUMP = 8,
    LAND = 9,
    DAMAGED = 10,
    DIE = 11,
    MELEE_ATTACK_END = 12,
}

public partial class AmonData
{
    // TODO: 타임라인 끝나면 Transform에 플레이어 넣어주기
    public Transform Target { get; set; } = null;

    public AnimatorCtrl<Amon_Animation_State> AnimatorCtrl{ get; set; }

    [SerializeField]
    private Bullet _amonProjectileBullet;
    public Bullet AmonProjectileBullet => _amonProjectileBullet;

    [SerializeField]
    private GameObject _amonProjectileParticle;
    public GameObject AmonProjectileParticle;

    [SerializeField]
    private Transform _amonProjectileAttackPos;
    public Transform AmonProjectileAttackPos => _amonProjectileAttackPos;

    [SerializeField]
    private float _projectileBulletSpeed = 40f;
    public float ProjectileBulletSpeed => _projectileBulletSpeed;
}