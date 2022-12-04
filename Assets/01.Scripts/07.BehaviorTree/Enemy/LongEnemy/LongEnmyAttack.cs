using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Net.Http.Headers;

public class LongEnmyAttack : BT_Node
{

	private LongEnemyData _data;
	public LongEnmyAttack(BehaviorTree t, LongEnemyData data,List<BT_Node> c = null) : base(t, c)
	{
		_data = data;
	}

	protected override void OnEnter()
	{
		_tree.transform.DOLocalRotate(new Vector3(_tree.transform.localRotation.x + 30, _tree.transform.localRotation.y, _tree.transform.localRotation.z), _data.BackSpeed).OnComplete(() =>
		{
			_tree.transform.DOLocalRotate(new Vector3(_tree.transform.localRotation.x - 30, _tree.transform.localRotation.y, _tree.transform.localRotation.z), _data.UpSpeed).OnComplete(() =>
			{

			});
		});
	}

	private void Fire()
	{
		//GameObject obj = ObjectPool.Instance.GetObject()
	}
	public override Result Execute()
	{
		return base.Execute();
	}
}

public partial class LongEnemyData
{
	[SerializeField]
	private float backSpeed;
	[SerializeField]
	private float upSpeed;
	public float BackSpeed => backSpeed;

	public float UpSpeed => upSpeed;
}
