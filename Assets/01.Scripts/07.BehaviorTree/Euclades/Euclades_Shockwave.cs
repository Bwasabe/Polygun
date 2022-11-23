using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System;
//using DaeHyunLibrary;
using UnityEngine;

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

        _seq = DOTween.Sequence();
        for(int i =0; i<_data.upDownCount; i++)
        {
			AddUpDownDotween(ref _seq, _data.Speeds[i].first, _data.Speeds[i].secound);
		}
		_seq.AppendCallback(() =>
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

    private void AddUpDownDotween(ref Sequence seq, float upSpeed, float downSpeed)
	{
        seq.Append(_tree.transform.DOMoveY(_data.UpDuration, upSpeed))
       .Join(_tree.transform.DORotate(new Vector3(0, 360, 0), upSpeed, RotateMode.FastBeyond360))
       .Append(_tree.transform.DOMoveY(_tree.transform.localScale.y / 2, downSpeed))
       .Join(_tree.transform.DORotate(new Vector3(0, 0, 0), downSpeed, RotateMode.FastBeyond360))
       .AppendCallback(() =>
       {
           GameObject obj = ObjectPool.Instance.GetObject(PoolObjectType.ShockWave);
           obj.transform.position = new Vector3(this._tree.transform.position.x, obj.transform.position.y, _tree.transform.position.z);
           obj.GetComponent<BlastWave>().StartExplosion();
           obj.GetComponentInChildren<ShorkWaveCollision>().damage = _data.ShorkWaveDamage;
       });
	}
}

public partial class Euclades_Data
{
    [SerializeField]
    private float _upDuration;

    public float UpDuration => _upDuration;

    [SerializeField]
    private Pair<float, float>[] _speeds;

    public Pair<float, float>[] Speeds => _speeds;
    public float upDownCount => _speeds.Length;

    [SerializeField]
    private float shorkWaveDamage;

    public float ShorkWaveDamage => shorkWaveDamage;
}

[Serializable]
public class Pair<T, U>
{
	public Pair()
	{
	}
	public Pair(T first, U second)
	{
		this.first = first;
		this.secound = second;
	}

	public T first;
	public U secound;
}
