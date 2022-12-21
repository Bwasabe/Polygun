using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Timeline;
using UnityEngine;

public class MeleeEnemyDamaged : BaseEnemyDamaged
{
	[SerializeField]
	private Renderer _dissolve;
	[SerializeField]
	private GameObject _slider;
	[SerializeField]
	private CharacterController characterController;
	[SerializeField]
	private CollisionCtrl _collisionCtrl;
	[SerializeField]
	private EnemyFollow _enemyFollow;

	private MeleeEnemy _testEnemy;
	private Material _material;
	private Animator _enemyAnimator;

	private bool isDie = false;
	private float _CureentTime = 0f;
	protected override void Awake()
	{
		isDie = false;
	}
	protected override void Start()
	{
		_testEnemy = GetComponent<MeleeEnemy>();
		_enemyAnimator = GetComponent<Animator>();
		RegisterStat();
		_stat.Init();
		_material = _dissolve.material;
	}
	protected override void RegisterStat()
	{
		_stat = _testEnemy.stat;
	}

	private void Update()
	{
		if(isDie)
		{
			_CureentTime += Time.deltaTime / 2;
			_material.SetFloat("_Dissolve", _CureentTime);
		}
		if(_material.GetFloat("_Dissolve") >= 1)
			Die();

	}

	public override void Damage(float damage)
	{
		_material.SetFloat("_Dissolve", 0);
		_stat.Damaged(damage);
		if (_stat.HP <= 0)
		{
			_enemyFollow.enabled = false;
			_collisionCtrl.gameObject.SetActive(false);
			characterController.enabled = false;
			_enemyAnimator.enabled = false;
			_testEnemy.enabled = false;
			_testEnemy.IsStop = true;
			_slider.SetActive(false);
			isDie = true;
		}
	}
}
