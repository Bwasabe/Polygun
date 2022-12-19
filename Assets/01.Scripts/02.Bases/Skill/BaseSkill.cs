using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseSkill
{
    protected BaseEquipment _parent;
    
    public BaseSkill(BaseEquipment parent)
    {
        _parent = parent;
    }

    public T GetInterface<T>() where T : class
    {
        if(!typeof(T).IsInterface)
        {
            Debug.LogError($"Type {typeof(T)} is not InterFace");
            return null;
        }
        return this as T;
    }

    public abstract void Skill();
}
