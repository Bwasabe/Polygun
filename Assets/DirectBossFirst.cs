using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DirectBossFirst : MonoBehaviour
{
	[SerializeField]
	private CinemachineFreeLook _cine;
	[SerializeField]
	MapManager _mapManager;
	[SerializeField]
	PlayableDirector _playable;

	private BehaviorTree bt;
	private void Start()
	{
		bt = _mapManager.boss.GetComponent<BehaviorTree>();
	}
	public void CutSceneStart()
	{
		_playable.Play();
	}
	public void BossFirst()
	{
		_cine.LookAt = _mapManager.boss.transform;
		bt.IsStop = true;
	}

	public void BossEnd()
	{
		_cine.LookAt = GameManager.Instance.Player.transform;
		bt.IsStop = false;
	}
}
