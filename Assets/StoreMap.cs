using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
	private List<ItemObject> _itemObjects;
	[SerializeField]
	private Vector3[] _storeObjVec = new Vector3[3];

	[SerializeField]
	private int _confirmationObjectCount = 0;

	[SerializeField]
	private CollisionCtrl _collisiionCtrl;

	[SerializeField]
	private LayerMask _layerMask;
	protected override void OnStart()
	{
	}
	protected override void OnEnter()
	{
		RandomObjs();
		_collisiionCtrl.ColliderEnterEvent += GroundOut;
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
		for (int i = 0; i < _confirmationObjectCount; i++)
		{
			GameObject obj = Instantiate(_itemObjects[i].obj, transform);
			obj.transform.localPosition = _storeObjVec[i];
			_itemObjects.RemoveAt(i);
		}

		for (int i = _confirmationObjectCount; i <= _storeObjVec.Length - 1; i++)
		{
			int rand = UnityEngine.Random.Range(0, _itemObjects.Count - 1);
			GameObject obj = Instantiate(_itemObjects[rand].obj, transform);
			obj.transform.localPosition = _storeObjVec[i];
			_itemObjects.RemoveAt(rand);
		}
	}
	private void GroundOut(Collider col)
	{
		if (((1 << col.gameObject.layer) & _layerMask) > 0)
		{
			Debug.Log("?");
			col.GetComponent<CharacterController>().enabled = false;
			col.transform.localPosition = new Vector3(121.6f, 0,-13.4f);
			col.GetComponent<IDmgAble>().Damage(5f);
			col.GetComponent<CharacterController>().enabled = true;
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
