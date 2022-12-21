using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DirectBossFirst : MonoBehaviour
{
	[SerializeField]
	private CinemachineVirtualCamera _cine;
	[SerializeField]
	private BehaviorTree bt;
	[SerializeField]
	PlayableDirector _playable;
	public void CutSceneStart()
	{
		_playable.Play();
	}
	public void BossFirst()
	{
		bt.IsStop = true;
	}

	public void BossEnd()
	{
		_cine.gameObject.SetActive(false);
		bt.IsStop = false;
	}
}
