using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class BoomEnemyAttack : BT_Node
{
	private Animator _animator;
	private BoomEnemy _info;
	private BoomEnemyData _data;
	public BoomEnemyAttack(BehaviorTree t, List<BT_Node> c = null) : base(t, c)
	{
		_animator = _tree.GetComponent<Animator>();
		_info = _tree as BoomEnemy;
		_data = _tree.GetData<BoomEnemyData>();
		_info.endAnimation = EndAttackAnimation;
	}

	protected override void OnEnter()
	{
		base.OnEnter();
		_animator.SetTrigger("attack");
		SoundManager.Instance.Play(AudioType.SFX, _data.boomStartClip);
	}

	public void EndAttackAnimation()
	{
		SoundManager.Instance.Play(AudioType.SFX, _data.boomClip);
		Collider[] hitColider = Physics.OverlapSphere(this._tree.transform.position, _data.Radius, _data.PlayerLayerMask);
		if (hitColider.Length > 0)
			hitColider[0]?.GetComponent<IDmgAble>().Damage(_data.Stat.DamageStat);
		UpdateState = UpdateState.Exit;
		_tree.gameObject.SetActive(false);
	}
}

public partial class BoomEnemyData
{
	[SerializeField]
	private float _radius;
	[SerializeField]
	private LayerMask _playerLayerMask;

	public AudioClip boomClip;
	public AudioClip boomStartClip;

	public LayerMask PlayerLayerMask => _playerLayerMask;
	public float Radius => _radius;
}
