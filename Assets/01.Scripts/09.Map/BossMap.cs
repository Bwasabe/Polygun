using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMap : MapSetting
{
	[SerializeField]
	private GameObject bossObject;

	private GameObject bossObj;
	protected override void OnStart()
	{
		bossObj = Instantiate(bossObject);
		bossObj.transform.parent = this.transform;
		bossObj.transform.localPosition = new Vector3(-11, 0, 6);
		bossObj.SetActive(false);
	}
	protected override void OnEnter()
	{
		bossObj.SetActive(true);
	}

	protected override void OnPlay()
	{

	}

	protected override bool OnIsEnter()
	{
		return this.transform.childCount >= 4;
	}
}
