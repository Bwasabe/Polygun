using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class NormalMap1 : MapSetting
{
	[SerializeField]
	private List<GameObject> enemys;

	protected override void OnStart()
	{
		foreach (GameObject obj in enemys)
		{
			obj.SetActive(false);
		}
	}
	protected override void OnEnter()
	{
		base.OnEnter();
		foreach(GameObject obj in enemys)
		{
			obj.SetActive(true);
		}
	}

	protected override void OnPlay()
	{
		int count = 0;
		foreach (GameObject obj in enemys)
		{
			if (!obj.gameObject.active)
				count++;
		}

		if (count == enemys.Count)
			MapState = MapState.End;
	}

	protected override bool OnIsEnter()
	{
		return transform.Find("Player") != null;
	}
}
