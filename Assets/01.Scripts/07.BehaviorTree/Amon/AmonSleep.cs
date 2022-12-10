using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Yields;

public class AmonSleep : BT_Condition
{
    private AmonData _data;
    private float _timer;
    public AmonSleep(BehaviorTree t, List<BT_Node> c = null) : base(t, c)
    {
        _data = _tree.GetData<AmonData>();
    }

    public override Result Execute()
    {
        return base.Execute();
    }

    protected override void OnEnter()
    {
        _timer = 0f;
        // TODO : 애니메이션 플레이
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.SLEEP_START);
        base.OnEnter();
    }

    protected override void OnUpdate()
    {
        _timer += Time.deltaTime;
        if(_timer >= _data.SleepDuration)
        {
            UpdateState = UpdateState.Exit;
        }
    }
    protected override void OnExit()
    {
        _data.AnimatorCtrl.SetAnimationState(Amon_Animation_State.SLEEP_END);
        _tree.IsStop = true;
        _tree.StartCoroutine(SleepEnd());
    }

    private IEnumerator SleepEnd()
    {
        yield return WaitForSeconds(_data.SleepEndAnimationClip.length);
        base.OnExit();
        _tree.IsStop = false;
    }

}

public partial class AmonData
{
    public bool IsSleep { get; set; } = false;

    [SerializeField]
    private float _sleepDuration = 3f;
    public float SleepDuration => _sleepDuration;

    [SerializeField]
    private AnimationClip _sleepEndAnimationClip;
    public AnimationClip SleepEndAnimationClip => _sleepEndAnimationClip;

    
    [SerializeField]
    private float _sleepEndDuration;
}
