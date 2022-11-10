using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorTree : MonoBehaviour
{
    private Dictionary<string, object> DataDict { get; } = new Dictionary<string, object>();

    private BT_Node _root;

    protected virtual void Start()
    {
        _root = SetupTree();
    }

    protected virtual void Update() {
        if(_root != null)
        {
            _root.Execute();
        }
    }


    public object GetData(string key)
    {
        if(DataDict.TryGetValue(key, out object o))
        {
            return o;
        }
        else
        {
            throw new System.Exception($"{key} is Null in Dict");
        }
    }
    public void SetData(string key, object o)
    {
        DataDict[key] = o;
    }

    protected abstract BT_Node SetupTree();

}
