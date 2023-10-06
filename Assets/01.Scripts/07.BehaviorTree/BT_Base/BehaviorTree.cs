using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public abstract class BehaviorTree : MonoBehaviour
{
    private Dictionary<Type, BT_Data> _dataDict = new Dictionary<Type, BT_Data>();

    protected BT_Node _root;

    // protected BT_Node _currentNode;

    public bool IsStop { get; set; } = false;

    protected virtual void Awake()
    {
        InitAllData();
    }
    protected virtual void Start()
    {
        _root = SetupTree();
    }

    private void InitAllData()
    {
        Type myType = this.GetType();
        BindingFlags flag = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
        FieldInfo[] fieldInfos = myType.GetFields(flag);

        foreach (var field in fieldInfos)
        {

            if(field.GetValue(this) is BT_Data data)
            {
                _dataDict.Add(field.FieldType, data);
            }
        }
    }

    protected virtual void Update()
    {
        if (_root != null && !IsStop)
        {
            _root.Execute();
        }
    }

    public void EnterNode()
    {
        _root.EnterNode();
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

    // protected void SetData(Type key, BT_Data data)
    // {
    //     _dataDict[key] = data;
    // }

    protected abstract BT_Node SetupTree();

}
