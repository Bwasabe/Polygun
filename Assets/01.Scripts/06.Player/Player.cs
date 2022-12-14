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
    ATTACK = 1 << 3,
    HIT = 1 << 4,
    DIE = 1 << 5,
    INVINCIBLE = 1 << 6,
}

[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    public PLAYER_STATE CurrentState { get; set; }

    public PlayerStat PlayerStat => _playerStat;
    [SerializeField]
    private PlayerStat _playerStat;


    private Dictionary<Type, BasePlayerComponent> _playerComponentDict = new Dictionary<Type, BasePlayerComponent>();

    private void Awake()
    {
        _playerStat.Init();
        
        BasePlayerComponent[] components = GetComponentsInChildren<BasePlayerComponent>();
        for (int i = 0; i < components.Length; ++i)
        {
            _playerComponentDict[components[i].GetType()] = components[i];
        }
    }

    // private void Start() {
    //     OnGUIManager.Instance._guiDict.Add("PLAYER_STATE", "");
    // }

    // private void Update() {
    //     OnGUIManager.Instance._guiDict["PLAYER_STATE"] = CurrentState.ToString();
    // }

    public void SetPlayerComponent<T>(BasePlayerComponent playerComponent) where T : BasePlayerComponent
    {
        _playerComponentDict[typeof(T)] = playerComponent;
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

