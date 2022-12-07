using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LongEnmyAttack : BT_Node
{
	private LongEnemy enemyInfo;
	private LongEnemyData _data;
	private Transform _target;
	private Sequence _seq;

	public LongEnmyAttack(BehaviorTree t, Transform target, LongEnemyData data, List<BT_Node> c = null) : base(t, c)
	{
		enemyInfo = _tree as LongEnemy;
		_target = target;
		_data = data;
	}

	protected override void OnEnter()
	{
		if (_seq != null)
		{
			_seq.Kill();
		}
		_seq = DOTween.Sequence();
		_seq
			.Append(_tree.transform.DOLocalRotate(new Vector3(30, _tree.transform.localRotation.y, _tree.transform.localRotation.z), _data.BackSpeed))
			.Append(_tree.transform.DOLocalRotate(new Vector3(0, _tree.transform.localRotation.y, _tree.transform.localRotation.z), _data.UpSpeed))
			.AppendCallback(AttackTweenCallBack);
		_tree.IsStop = true;
		//base.OnEnter();
	}

	protected override void OnUpdate()
	{
		Debug.Log(_tree);
	}

	private void AttackTweenCallBack()
	{
		Fire();
		enemyInfo.IsAttack = false;
		UpdateState = UpdateState.Exit;
		_tree.IsStop = false;
	}
	private void Fire()
	{
		GameObject obj = ObjectPool.Instance.GetObject(PoolObjectType.LongEnemyBullet);
		obj.transform.position = _tree.transform.position;
		Bullet bullet = obj.GetComponent<Bullet>();
		bullet.HitLayer = _data.LayerMask;
		bullet.Direction = (_target.position - _tree.transform.position).normalized;
		bullet.Damage = _data.Stat.DamageStat;
		bullet.Speed = _data.BulletSpeed;
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
}

public partial class LongEnemyData
{
	[SerializeField]
	private float backSpeed;
	[SerializeField]
	private float upSpeed;
	[SerializeField]
	private LayerMask layer;
	[SerializeField]
	private float bulletSpeed;

	public float BulletSpeed => bulletSpeed;
	public LayerMask LayerMask => layer;
	public float BackSpeed => backSpeed;

	public float UpSpeed => upSpeed;
}
