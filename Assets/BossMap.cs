using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMap : MapSetting
{
	[SerializeField]
	private GameObject bossObject;
	protected override void OnStart()
	{
		GameObject bossObj = Instantiate(bossObject);
		bossObj.transform.position = new Vector3(-11, 0, 6);
		bossObj.SetActive(false);
	}
	protected override void OnEnter()
	{
		GameObject obj = GameObject.Find("BossSlider");
		obj.SetActive(true);
	}

	protected override void OnPlay()
	{

	}
}
