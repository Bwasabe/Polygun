using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMap : MapSetting
{
	[SerializeField]
	private GameObject bossObject;

	private DirectBossFirst _direct;
	private GameObject bossObj;
	public GameObject Boss => bossObj;
	protected override void OnStart()
	{
		bossObj = Instantiate(bossObject);
		bossObj.transform.parent = this.transform;
		bossObj.transform.localPosition = new Vector3(-11, 0, 6);
		bossObj.SetActive(false);
		_direct = GameObject.Find("Direct").gameObject.GetComponent<DirectBossFirst>();
	}
	protected override void OnEnter()
	{
		bossObj.SetActive(true);
		_direct.CutSceneStart();
	}

	protected override void OnPlay()
	{
		if (!bossObj.activeSelf)
			MapState = MapState.End;
	}
}
