using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static Yields;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEditor;

public class LongEnmyAttack : BT_Node
{
	private LongEnemy enemyInfo;
	private LongEnemyData _data;
	private Transform _target;
	private Sequence _seq;
	private Coroutine c;
	public LongEnmyAttack(BehaviorTree t, Transform target, LongEnemyData data, List<BT_Node> c = null) : base(t, c)
	{
		enemyInfo = _tree as LongEnemy;
		_target = target;
		_data = data;
		enemyInfo.deadAction += Reset;
	}
	private void Reset()
	{
		Debug.Log("?");
		_tree.StopCoroutine(c);
		//_seq.Kill();
	}
	protected override void OnEnter()
	{
		//if (_seq != null)
		//{
		//	_seq.Kill();
		//}
		//_seq = DOTween.Sequence();
		//_seq
		//	.Append(_tree.transform.DOLocalRotate(new Vector3(_tree.transform.localRotation.x, _tree.transform.localRotation.y, -30), _data.BackSpeed))
		//	.Append(_tree.transform.DOLocalRotate(new Vector3(_tree.transform.localRotation.x, _tree.transform.localRotation.y, 0), _data.UpSpeed))
		//	.AppendCallback(AttackTweenCallBack);
		//_tree.IsStop = true;
		base.OnEnter();
	}

	bool isAttackRotation = false;
	protected override void OnUpdate()
	{
		NodeResult = Result.RUNNING;
		Vector3 rotation = _target.transform.position - _tree.transform.position;

		if (_tree.transform.localRotation.x <= -30f && !isAttackRotation)
		{
			isAttackRotation = true;
		}
		else if(_tree.transform.localRotation.x >= 0f && isAttackRotation)
		{
			isAttackRotation = false;
			AttackTweenCallBack();
		}

		if (!isAttackRotation)
			rotation.x += -Time.deltaTime;
		else
			rotation.x += Time.deltaTime;

		_tree.transform.LookAt(rotation);
		Debug.Log("øÕ¿Ã≥¥");
		//_tree.transform.rotation = Quaternion.Slerp(_tree.transform.rotation, Quaternion.LookRotation(rotation), _data.BulletWait/2);
	}

	private void AttackTweenCallBack()
	{
		c = _tree.StartCoroutine(FireWait());
	}

	private IEnumerator FireWait()
	{
		GameObject obj = null;
		Fire(ref obj);
		obj.transform.DOScale(0, 0);
		obj.transform.DOScale(1, _data.BulletWait);
		enemyInfo.IsAttack = true;
		yield return WaitForSeconds(_data.BulletWait);
		obj.GetComponent<Bullet>().Direction = (_target.position - _tree.transform.position).normalized;
		enemyInfo.IsAttack = false;
		UpdateState = UpdateState.Exit;
	}
	private void Fire(ref GameObject obj)
	{
		obj = ObjectPool.Instance.GetObject(PoolObjectType.LongEnemyBullet);
		obj.transform.position = _tree.transform.position;
		Bullet bullet = obj.GetComponent<Bullet>();
		bullet.HitLayer = _data.LayerMask;
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

	[SerializeField]
	private float bulletWait;

	public float BulletWait => bulletWait;

	public float BulletSpeed => bulletSpeed;
	public LayerMask LayerMask => layer;
	public float BackSpeed => backSpeed;

	public float UpSpeed => upSpeed;
}
