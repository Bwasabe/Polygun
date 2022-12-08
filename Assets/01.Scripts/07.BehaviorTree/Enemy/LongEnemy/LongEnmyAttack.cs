using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static Yields;


public class LongEnmyAttack : BT_Node
{
	private LongEnemy enemyInfo;
	private LongEnemyData _data;
	private Transform _target;

	private Coroutine c;

	private bool _isUp = false;

	private Quaternion _nowRotate;
	private Quaternion _targetRotate;

	private float _timer = 0f;
	public LongEnmyAttack(BehaviorTree t, Transform target, LongEnemyData data, List<BT_Node> c = null) : base(t, c)
	{
		enemyInfo = _tree as LongEnemy;
		_target = target;
		_data = data;
		enemyInfo.deadAction += Reset;
	}
	private void Reset()
	{
		_tree.StopCoroutine(c);
	}
	protected override void OnEnter()
	{
		enemyInfo.IsAttack = true;
		_isUp = true;

		Vector3 euler = Vector3.zero;
		_nowRotate = Quaternion.Euler(euler);
		euler.x -= _data.UpX;
		_targetRotate = Quaternion.Euler(euler);
		base.OnEnter();
	}

	protected override void OnUpdate()
	{
		_timer += Time.deltaTime;
		NodeResult = Result.RUNNING;

		_tree.transform.LookAt(_target.transform.position);

		if(_isUp)
		{
			_data.Model.transform.localRotation = Quaternion.Lerp(_nowRotate, _targetRotate, _timer / _data.RotateDuration);
		}
		else
		{
			_data.Model.transform.localRotation = Quaternion.Lerp(_targetRotate, _nowRotate, _timer / _data.RotateDuration);
		}

		if(_timer >= _data.RotateDuration)
		{
			if(_isUp)
			{
				_timer = 0f;
				_isUp = false;
			}
			else
			{
				Fire();
			}
			
		}
	}
	private void Fire()
	{
		GameObject obj = ObjectPool.Instance.GetObject(PoolObjectType.LongEnemyBullet);
		obj.transform.position = _tree.transform.position;
		Bullet bullet = obj.GetComponent<Bullet>();
		bullet.HitLayer = _data.LayerMask;
		bullet.Damage = _data.Stat.DamageStat;
		bullet.Speed = _data.BulletSpeed;
		bullet.Direction = (_target.position - obj.transform.position).normalized;
		UpdateState = UpdateState.Exit;
	}

	protected override void OnExit()
	{
		NodeResult = Result.SUCCESS;
		_timer = 0f;
		enemyInfo.IsAttack = false;
		_data.Model.transform.localRotation = Quaternion.identity;
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
	private LayerMask layer;
	[SerializeField]
	private float bulletSpeed;
	public float BulletSpeed => bulletSpeed;
	public LayerMask LayerMask => layer;

	[SerializeField]
	private float _upX = 30f;
	public float UpX => _upX;

	[SerializeField]
	private float _rotateDuration = 1f;
	public float RotateDuration => _rotateDuration;

	[SerializeField]
	private Transform _model;
	public Transform Model => _model;
	[SerializeField]
	private float _rotateSmooth = 8f;
	public float RotateSmooth => _rotateSmooth;
}
