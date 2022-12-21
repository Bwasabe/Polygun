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
	[SerializeField]
	private GameObject lightobj;

	GameObject obj;
	public void CutSceneStart()
	{
		_playable.Play();
	}
	public void BossFirst()
	{
		bt.IsStop = true;
		obj = GameObject.Find("Player");
		obj.SetActive(false);
		lightobj.SetActive(true);
	}

	public void BossEnd()
	{
		_cine.gameObject.SetActive(false);
		bt.IsStop = false;
		obj.SetActive(true);
	}
}
