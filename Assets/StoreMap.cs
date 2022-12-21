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
    private List<Transform> _storeObj;

    [SerializeField]
    private float _height = 2f;

    [SerializeField]
    private int _confirmationObjectCount = 0;

    [SerializeField]
    private float _lookSmooth = 8f;

    [SerializeField]
    private Transform _muryotaisu;

    private Transform _target;


    protected override void Update()
    {
        base.Update();
        if (_target != null)
        {
            Vector3 lookdir = _target.position - _muryotaisu.position;
            lookdir.y = 0f;
            _muryotaisu.rotation = Quaternion.LookRotation(lookdir * Time.deltaTime * _lookSmooth);
        }

    }

    protected override void OnEnter()
    {
        SpawnItems();
    }
    public override void RepeatOnEnter()
    {
        // TODO: 사운드
        base.RepeatOnEnter();
        _target = GameManager.Instance.Player.transform;
        //���� ������ ���� �ϱ�
        //SoundManager.Instance.Play(AudioType.Voice, _welcomeAudios[Random.Range(0, _welcomeAudios.Count)]);
    }
    private void SpawnItems()
    {
        for (int i = 0; i < _itemObjects.Count; ++i)
        {
            Debug.Log(">");
            Vector3 storePos = _storeObj[i].position;
            storePos.y += _height;

            GameObject obj = Instantiate(_itemObjects[i].obj, storePos, Quaternion.identity, transform);
            if(_itemObjects[i].ObjsType.Equals(StoreObjs.MIDDLEHPHEAL) || _itemObjects[i].ObjsType.Equals(StoreObjs.SMALLHPHEAL))
                obj.GetComponentInChildren<HealPortion>().IsShopPotion = true;

        }

    }

	protected override void OnExit()
	{
		_target = null;
	}

	private void RandomObjs()
    {
        for (int i = 0; i < _confirmationObjectCount; i++)
        {
            GameObject obj = Instantiate(_itemObjects[i].obj, transform);
            // obj.transform.localPosition = _storeObjVec[i];
            //_itemObjects.RemoveAt(i);
        }

        // for (int i = _confirmationObjectCount; i <= _storeObjVec.Length - 1; i++)
        // {
        // 	int rand = UnityEngine.Random.Range(0, _itemObjects.Count - 1);
        // 	GameObject obj = Instantiate(_itemObjects[rand].obj, transform);
        // 	// obj.transform.localPosition = _storeObjVec[i];
        // 	_itemObjects.RemoveAt(rand);
        // }
    }

}

[Serializable]
public struct ItemObject
{
    public StoreObjs ObjsType;
    public GameObject obj;
    public bool isDelete;
}