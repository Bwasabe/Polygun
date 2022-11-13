using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum PLAYER_STATE
{
    IDLE = 1 << 0,
    MOVE = 1 << 1,
    JUMP = 1 << 2,
    HOOK = 1 << 3,
    HIT = 1 << 4,
    DIE = 1 << 5
}

[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    public PLAYER_STATE CurrentState { get; set; }

    [SerializeField]
    public PlayerStat playerStat;


    private Dictionary<Type, BasePlayerComponent> _playerComponentDict = new Dictionary<Type, BasePlayerComponent>();

    private void Awake()
    {
        BasePlayerComponent[] components = GetComponentsInChildren<BasePlayerComponent>();
        for (int i = 0; i < components.Length; ++i)
        {
            _playerComponentDict[components[i].GetType()] = components[i];
        }
    }

    public void AddPlayerComponent<T>() where T : BasePlayerComponent
    {

    }

    public T GetPlayerComponent<T>() where T : BasePlayerComponent
    {
        Type type = typeof(T);
        if (_playerComponentDict.TryGetValue(type, out BasePlayerComponent data))
        {
            return data as T;
        }
        else
        {
            throw new System.Exception($"{type} is Null in Dict");
        }
    }
}

