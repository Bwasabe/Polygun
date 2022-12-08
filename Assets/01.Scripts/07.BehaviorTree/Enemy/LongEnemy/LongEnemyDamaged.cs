using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongEnemyDamaged : BaseEnemyDamaged
{
	[SerializeField]
	private Renderer _dissolve;
	[SerializeField]
	private GameObject _slider;
	[SerializeField]
	private Animation _enemyAnimation;

	private CharacterController _characterController;
	private LongEnemy _testEnemy;
	private Material _material;

	private bool isDie = false;
	private float _CureentTime = 0f;
	protected override void Awake()
	{
		isDie = false;
	}
	protected override void Start()
	{
		_characterController = GetComponent<CharacterController>();
		_testEnemy = GetComponent<LongEnemy>();
		//_enemyAnimator = GetComponent<Animator>();
		RegisterStat();
		_stat.Init();
		_material = _dissolve.material;
	}
	protected override void RegisterStat()
	{
		_stat = _testEnemy.longEnemyData;
	}

	private void Update()
	{
		if (isDie)
		{
			_CureentTime += Time.deltaTime / 2;
			_material.SetFloat("_Dissolve", _CureentTime);
		}
		if (_material.GetFloat("_Dissolve") >= 1)
			Die();

	}

	public override void Damage(float damage)
	{
		_material.SetFloat("_Dissolve", 0);
		_stat.Damaged(damage);
		if (_stat.HP <= 0)
		{
			_characterController.enabled = false;
			_enemyAnimation.enabled = false;
			_testEnemy.enabled = false;
			_testEnemy.IsStop = true;
			_slider.SetActive(false);
			isDie = true;
		}
	}
}
