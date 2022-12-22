using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;

public enum ChestType
{
	Gold,
	SmallHP,
}
public class Chest : MonoBehaviour
{
	[SerializeField]
	private List<ChestObject> objs;
	private PlayableDirector playableDirector;

	[HideInInspector]
	public bool isOpen = false;

	private void Start()
	{
		objs.Sort(delegate (ChestObject data1, ChestObject data2)
		{
			if (data1.percent > data2.percent)
				return 1;
			else
				return -1;
		});
		playableDirector = GetComponent<PlayableDirector>();
	}
	public void OpenBox()
	{
		if(!isOpen)
		{
			isOpen = true;
			playableDirector.Play();
		}
	}

	public void OpenEnd()
	{
		float rand = UnityEngine.Random.Range(0, 100);
		GameObject obj = null;
		for (int i = 0; i< objs.Count; i++)
		{
			if (objs[i].percent > rand)
			{
				obj = Instantiate(objs[i].obj);
				obj.transform.position = this.transform.position + Vector3.up;
				return;
			}
		}

		obj = Instantiate(objs[0].obj);
        HealPortion portion = obj.GetComponent<HealPortion>();
		if(portion != null)
        	portion.IsShopItem = false;
			
        obj.transform.position = this.transform.position + Vector3.up;
	}
}

[Serializable]
public struct ChestObject
{
	public GameObject obj;
	public float percent;
}
