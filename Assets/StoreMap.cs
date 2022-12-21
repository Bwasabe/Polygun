using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    private List<AudioClip> _welcomeAudios;
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
		// RandomObjs();
	}

	protected override void OnExit()
	{
		
	}

	private void Update() {
		
	}

	protected override void OnPlay()
	{
		//���⼭ ��� �ִ� ĳ���Ͱ� ���ų� �����մϴ� �ϱ�
	}

	protected override bool OnIsEnter()
	{
		return base.OnIsEnter();
	}
	public override void RepeatOnEnter()
	{
        //���� ������ ���� �ϱ�
        //SoundManager.Instance.Play(AudioType.Voice, _welcomeAudios[Random.Range(0, _welcomeAudios.Count)]);
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

}

[Serializable]
public struct ItemObject
{
	public StoreObjs ObjsType;
	public GameObject obj;
	public bool isDelete;
}
