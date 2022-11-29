using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SKillType
{
    // 좌클
    MainAttack,
    // LShift
    SubSkill,
    
}

public abstract class BaseSkill
{
    protected BaseSkill(SKillType sKillType) {
        SKillType = sKillType;
    }

    public SKillType SKillType{ get; set; }
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
