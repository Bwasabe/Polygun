using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleton<ObjectPool>
{

    [SerializeField]
    ObjectPoolData objectPoolData;

    Dictionary<PoolObjectType, Queue<GameObject>> poolObjectMap = new Dictionary<PoolObjectType, Queue<GameObject>>();

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < objectPoolData.prefabs.Count; i++)
        {
            poolObjectMap.Add((PoolObjectType)i, new Queue<GameObject>());

            for (int j = 0; j < objectPoolData.prefabCreateCounts[i]; j++)
                poolObjectMap[(PoolObjectType)i].Enqueue(CreateNewObject(i));
        }
    }

    private GameObject CreateNewObject(int index)
    {
        var newObj = Instantiate(objectPoolData.prefabs[index]);
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public GameObject GetObject(PoolObjectType type, bool isActive = true)
    {
        if (Instance.poolObjectMap[type].Count > 0)
        {
            var obj = Instance.poolObjectMap[type].Dequeue();
            Debug.Log(transform);
            obj.transform.SetParent(transform);
            obj.gameObject.SetActive(isActive);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject((int)type);
            newObj.gameObject.SetActive(isActive);
            newObj.transform.SetParent(transform);

            return newObj;
        }
    }

    public void ReturnObject(PoolObjectType type, GameObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolObjectMap[type].Enqueue(obj);
    }
    
	public IEnumerator ReturnObject(PoolObjectType type, GameObject returnObject, float duration)
	{
        yield return Yields.WaitForSeconds(duration);
        ReturnObject(type, returnObject);
    }
}