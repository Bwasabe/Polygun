using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Yields;

public class AmonProjectileAttack : BT_Node
{
    private AmonData _data;
    private float _timer;

    public AmonProjectileAttack(BehaviorTree t, List<BT_Node> c = null) : base(t, c)
    {
        _data = _tree.GetData<AmonData>();
    }


    private const string PROJECTILE_ATTACK = "attack_02";

    public override Result Execute()
    {
        return base.Execute();
    }

    protected override void OnEnter()
    {
        Debug.Log("엔터");
        NodeResult = Result.RUNNING;
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.IDLE);
        base.OnEnter();
        // _tree.StartCoroutine(PlayAnimation());
    }

    protected override void OnUpdate()
    {
        Debug.Log("업데이트");

        _timer += Time.deltaTime;
        Vector3 dir = _data.Target.position - _tree.transform.position;
        if(dir != Vector3.zero)
        {
            _tree.transform.rotation = Quaternion.Slerp(_tree.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * _data.RotateSmooth) ;
            _tree.transform.rotation = Quaternion.Euler(0f, _tree.transform.eulerAngles.y, 0f);
        }

        if (_timer >= _data.ProjectileAttackDelay)
        {
            _tree.StartCoroutine(PlayAnimation());
            // UpdateState = UpdateState.Exit;
        }
    }

    protected override void OnExit()
    {
        base.OnExit();
        _timer = 0f;
        // _tree.StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        _tree.IsStop = true;
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.PROJECTILE_ATTACK);
        yield return WaitForSeconds(_data.AnimatorCtrl.GetAnimationLength(PROJECTILE_ATTACK) + _data.ProjectileAttackEndDelay);
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.IDLE);
        UpdateState = UpdateState.Exit;
        _tree.IsStop = false;
        NodeResult = Result.SUCCESS;
    }

}

public partial class AmonData
{
    [SerializeField]
    private float _projectileAttackDelay = 2f;
    public float ProjectileAttackDelay => _projectileAttackDelay;

    [SerializeField]
    private float _projectileAttackEndDelay = 1f;
    public float ProjectileAttackEndDelay => _projectileAttackEndDelay;
}
