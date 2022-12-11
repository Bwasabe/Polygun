using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StoreObjs
{
	SMALLHPHEAL,
	MIDDLEHPHEAL,
	CHRONOS,
	ASSASSIN,
	Count
}
public class StoreMap : MapSetting
{
	[SerializeField]
	private List<ItemObject> itemObjects;
	[SerializeField]
	private Vector3[] storeObjVec = new Vector3[3];

	[SerializeField]
	private int confirmationObjectCount = 0;
	protected override void OnStart()
	{

	}
	protected override void OnEnter()
	{
		RandomObjs();
	}

	protected override void OnPlay()
	{

	}

	protected override bool OnIsEnter()
	{
		return base.OnIsEnter();
	}

	private void RandomObjs()
	{
		for(int i = 0; i<confirmationObjectCount; i++)
		{
			GameObject obj = Instantiate(itemObjects[i].obj, transform);
			obj.transform.localPosition = storeObjVec[i];
			itemObjects.RemoveAt(i);
		}

		for (int i = confirmationObjectCount; i<= storeObjVec.Length - 1; i++)
		{
			int rand = UnityEngine.Random.Range(0, itemObjects.Count-1);
			GameObject obj = Instantiate(itemObjects[rand].obj, transform);
			obj.transform.localPosition = storeObjVec[i];
			itemObjects.RemoveAt(rand);
		}
	}
}

[Serializable]
public struct ItemObject
{
	public StoreObjs ObjsType;
	public GameObject obj;
	public int cost;
	public bool isDelete;
}
