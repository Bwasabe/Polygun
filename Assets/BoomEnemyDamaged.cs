using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEnemyDamaged : BaseEnemyDamaged
{
	[SerializeField]
	private Renderer _dissolve;
	[SerializeField]
	private GameObject _slider;
	[SerializeField]
	private CharacterController characterController;

	private BoomEnemy _testEnemy;
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
		_testEnemy = GetComponent<BoomEnemy>();
		_enemyAnimator = GetComponent<Animator>();
		RegisterStat();
		_stat.Init();
		_material = _dissolve.material;
	}
	protected override void RegisterStat()
	{
		_stat = _testEnemy._stat;
	}

	private void Update()
	{
		if (isDie)
		{
			_CureentTime += Time.deltaTime/2;
			_dissolve.material.SetFloat("_Dissolve", _CureentTime);
		}
		if (_dissolve.material.GetFloat("_Dissolve") >= 1)
			Die();

	}

	public override void Damage(float damage)
	{
		//_material.SetFloat("_Dissolve", 0);
		_stat.Damaged(damage);
		SoundManager.Instance.Play(AudioType.SFX, _hitSound);
		if (_stat.HP <= 0)
		{
			characterController.enabled = false;
			_enemyAnimator.enabled = false;
			_testEnemy.enabled = false;
			_testEnemy.IsStop = true;
			_slider.SetActive(false);
			isDie = true;
		}
	}
}
