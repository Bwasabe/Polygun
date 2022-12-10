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
		GameObject obj = Instantiate(itemObjects[(int)StoreObjs.SMALLHPHEAL].obj,transform);
		obj.transform.localPosition = storeObjVec[0];
		itemObjects.RemoveAt((int)StoreObjs.SMALLHPHEAL);
		for (int i = 1; i<= storeObjVec.Length - 1; i++)
		{
			int rand = UnityEngine.Random.Range(0, itemObjects.Count-1);
			obj = Instantiate(itemObjects[rand].obj, transform);
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
