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

	public void CutSceneStart()
	{
		_playable.Play();
	}
	public void BossFirst()
	{
		Debug.Log(_mapManager.boss);
		_cine.LookAt = _mapManager.boss.transform;
	}

	public void BossEnd()
	{
		_cine.LookAt = GameManager.Instance.Player.transform;
	}
}
