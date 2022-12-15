using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class NormalMap : MapSetting
{
	[SerializeField]
	private List<GameObject> enemys;
	[SerializeField]
	private GameObject Chest;
	[SerializeField]
	private Transform chestPosition;

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
		foreach (GameObject obj in enemys)
		{
			obj.SetActive(true);
		}
	}

	protected override void OnPlay()
	{
		int count = 0;
		foreach (GameObject obj in enemys)
		{
			if (!obj.gameObject.activeSelf)
				count++;
		}

		if (count == enemys.Count)
		{
			GameObject chest = Instantiate(Chest,transform.parent);
			chest.transform.localPosition = chestPosition.localPosition;
			MapState = MapState.End;
		}
	}

	protected override bool OnIsEnter()
	{
		return transform.Find("Player") != null;
	}
}
