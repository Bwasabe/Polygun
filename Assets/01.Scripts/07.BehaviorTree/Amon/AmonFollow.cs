using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
// using UnityEngine.AI;

public class AmonFollow : BT_Node
{
    private CharacterController _cc;
    // private NavMeshAgent _agent;
    private AmonData _data;

    private bool _isSummonBullet;
    private float _timer;
    private int _currentBulletIndex = 0;

    public AmonFollow(BehaviorTree t, List<BT_Node> c = null) : base(t, c)
    {
        _data = _tree.GetData<AmonData>();
        _cc = _data.CC;
        // _agent = _data.Agent;
    }

    public override Result Execute()
    {
        base.Execute();
        return NodeResult;
    }

    protected override void OnEnter()
    {
        if (_data.IsAttack)
        {
            UpdateState = UpdateState.Exit;
            return;
        }
        _isSummonBullet = true;
        _currentBulletIndex = 0;

        base.OnEnter();
        // _agent.speed = _data.MoveSpeed;
        NodeResult = Result.SUCCESS;
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.WALK);
    }

    protected override void OnExit()
    {
        base.OnExit();
    }

    protected override void OnUpdate()
    {
        if (_isSummonBullet)
        {
            if (_currentBulletIndex < _data.MeleeBulletPos.Count)
            {
                _timer += Time.deltaTime;
                if (_timer >= _data.MeleeBulletSpawnDuration)
                {
                    _timer = 0f;
                    //TODO: 풀링
                    GameObject obj = ObjectPool.Instance.GetObject(PoolObjectType.AmonMeleeBullet);
                    obj.transform.SetParent(_data.MeleeBulletPos[_currentBulletIndex]);
                    obj.transform.localPosition = Vector3.zero;
                    obj.transform.localRotation = Quaternion.identity;
                    obj.SetActive(true);

                    Bullet bullet = obj.GetComponent<Bullet>();
                    bullet.HitLayer = _data.HitLayer;
                    bullet.IsPlayerBullet = false;
                    bullet.Speed = 0;

                    // Debug.Break();
                    _data.MeleeBullets.Add(bullet.GetComponent<Bullet>());

                    if (_currentBulletIndex == _data.MeleeBulletPos.Count - 1)
                    {
                        bullet.transform.DOScale(Vector3.one, _data.MeleeBulletSpawnDuration * 0.5f).OnComplete(() =>
                        {
                            _isSummonBullet = false;
                        });
                    }
                    else
                    {
                        bullet.transform.DOScale(Vector3.one, _data.MeleeBulletSpawnDuration * 0.5f);
                    }

                    _currentBulletIndex++;

                }
            }
        }
        else
        {
            if (Vector3.Distance(_data.Target.position, _tree.transform.position) <= _data.AttackDistance)
            {
                NodeResult = Result.FAILURE;
                UpdateState = UpdateState.Exit;
                _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.IDLE);
                _data.IsAttack = true;
            }
            else
            {
                NodeResult = Result.SUCCESS;

                Vector3 dir = _data.Target.position - _tree.transform.position;
                dir.y = 0f;
                dir.Normalize();

                _cc.Move(dir * _data.MoveSpeed * Time.deltaTime);

                Vector3 lookDir = _data.Target.position - _tree.transform.position;
                if (lookDir != Vector3.zero)
                {
                    _tree.transform.rotation = Quaternion.Slerp(_tree.transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * _data.RotateSmooth);
                    _tree.transform.rotation = Quaternion.Euler(0f, _tree.transform.eulerAngles.y, 0f);
                }
                // _agent.SetDestination(_data.Target.position);
                // _cc.Move(_agent.velocity);
            }
        }
    }
}

public partial class AmonData
{
    public bool IsAttack { get; set; } = false;
    // public NavMeshAgent Agent{ get; set; }
    public CharacterController CC { get; set; }

    [SerializeField]
    private float _moveSpeed = 20f;
    public float MoveSpeed => _moveSpeed;

    [SerializeField]
    private float _attackDistance = 1f;
    public float AttackDistance => _attackDistance;

    [SerializeField]
    private List<Transform> _meleeBulletPos;
    public List<Transform> MeleeBulletPos => _meleeBulletPos;

    [SerializeField]
    private float _meleeBulletSpawnDuration = 0.2f;
    public float MeleeBulletSpawnDuration => _meleeBulletSpawnDuration;

    public List<Bullet> MeleeBullets { get; set; } = new List<Bullet>();

    [SerializeField]
    private GameObject _meleeBulletPrefab;
    public GameObject MeleeBulletPrefab => _meleeBulletPrefab;

    [SerializeField]
    private LayerMask _hitLayer;
    public LayerMask HitLayer => _hitLayer;

    [SerializeField]
    private LayerMask _groundLayer;
    public LayerMask GroundLayer => _groundLayer;
}
