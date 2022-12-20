using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMap : MapSetting
{
	[SerializeField]
	private GameObject bossObject;

	private DirectBossFirst _direct;


	//private GameObject bossObj;
	public GameObject Boss => bossObject;
	protected override void OnStart()
	{
		bossObject.SetActive(false);
		_direct = GameObject.Find("Direct").gameObject.GetComponent<DirectBossFirst>();
	}
	protected override void OnEnter()
	{
		bossObject.SetActive(true);
		_direct.CutSceneStart();
	}

	protected override void OnPlay()
	{
		if (!bossObject.activeSelf)
			MapState = MapState.End;
	}
}
