using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// TODO: 쇼크웨이브
public class Euclades_Shockwave : BT_Node
{
    public Euclades_Shockwave(BehaviorTree t , List<BT_Node> c = null) : base(t, c)
    {
        _data = _tree.GetData<Euclades_Data>();
	}

    private bool _isUp;

    private Sequence _seq = DOTween.Sequence();

	private Euclades_Data _data;

    private GameObject blastWave;
	protected override void OnEnter()
    {
		base.OnEnter();

		_seq = DOTween.Sequence()
		.Append(_tree.transform.DOMoveY(_data.UpDuration, _data.UpSpeed))
		.Join(_tree.transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360))
		.Append(_tree.transform.DOMoveY(_tree.transform.localScale.y / 2, _data.DownSpeed))
		.Join(_tree.transform.DORotate(new Vector3(0, 0, 0), 2f, RotateMode.FastBeyond360))
		.AppendCallback(() =>
		{

            UpdateState = UpdateState.Exit;
		});
	}

	protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    protected override void OnExit()
    {
        base.OnExit();
    }

    public override Result Execute()
    {
        base.Execute();
        return NodeResult;
    }
    // public override Result Execute()
    // {
    //     // 트윈이 끝나면 IsStop false, Random도 초기화
    //     _tree.IsStop = true;

    //     return Result.RUNNING;
    // }
}

public partial class Euclades_Data
{
    [SerializeField]
    private float _upDuration;

    public float UpDuration => _upDuration;

    [SerializeField]
    private float _upSpeed;

    public float UpSpeed => _upSpeed;

    [SerializeField]
    private float _downSpeed;
    public float DownSpeed => _downSpeed;
}
