using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public abstract class BehaviorTree : MonoBehaviour
{
    private Dictionary<Type, BT_Data> _dataDict = new Dictionary<Type, BT_Data>();

    private BT_Node _root;


    protected virtual void Start()
    {
        _root = SetupTree();
        InitAllData();
    }

    private void InitAllData()
    {
        Type myType = this.GetType();
        BindingFlags flag = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
        FieldInfo[] fieldInfos = myType.GetFields(flag);

        foreach (var field in fieldInfos)
        {
            BT_Data data = field.GetValue(this) as BT_Data;

            if(data != null)
            {
                Debug.Log(field);
                _dataDict.Add(field.FieldType, data);
            }
        }
    }

    protected virtual void Update()
    {
        if (_root != null)
        {
            _root.Execute();
        }
    }

    public T GetData<T>() where T : BT_Data
    {
        Type type = typeof(T);
        if (_dataDict.TryGetValue(type, out BT_Data data))
        {
            return data as T;
        }
        else
        {
            throw new System.Exception($"{type} is Null in Dict");
        }
    }

    public void SetData(Type key, BT_Data data)
    {
        _dataDict[key] = data;
    }

    protected abstract BT_Node SetupTree();

}
