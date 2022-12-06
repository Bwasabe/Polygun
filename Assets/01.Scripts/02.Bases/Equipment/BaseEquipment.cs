using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public abstract class BaseEquipment : MonoBehaviour
{
    [SerializeField]
    private float _sinCycle = 1.5f;
    [SerializeField]
    private float _height = 0.5f;
    [SerializeField]
    protected GameObject _particle;

    protected BaseSkill _attack;
    public BaseSkill Attack => _attack;
    protected BaseSkill _subSkill;
    public BaseSkill SubSkill => _subSkill;

    public bool IsEquip { get; set; } = false;

    private Dictionary<System.Type, BaseEquipmentData> _dataDict = new Dictionary<System.Type, BaseEquipmentData>();
    protected PlayerSkillCtrl _skillCtrl;

    private Vector3 _startPos;

    protected virtual void Awake() {
        InitAllData();
    }

    protected virtual void Start()
    {
        RegisterSkills();
        _skillCtrl = GameManager.Instance.Player.GetPlayerComponent<PlayerSkillCtrl>();
        _startPos = transform.position;

    }

    protected virtual void Update()
    {
        if(!IsEquip)
        {
            Vector3 pos = transform.position;
            pos.y = Mathf.Sin(Time.time * _sinCycle) * _height + _startPos.y;
            transform.position = pos;
        }

    }

    // 처음 스킬들을 등록시키는 함수(공격, sub)
    protected abstract void RegisterSkills();

    public virtual void RemoveParticle()
    {
        _particle.SetActive(false);
    }

    private void InitAllData()
    {
        Type myType = this.GetType();
        BindingFlags flag = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
        FieldInfo[] fieldInfos = myType.GetFields(flag);

        foreach (var field in fieldInfos)
        {
            BaseEquipmentData data = field.GetValue(this) as BaseEquipmentData;

            if(data != null)
            {
                _dataDict.Add(field.FieldType, data);
            }
        }
    }

    public T GetData<T>() where T : BaseEquipmentData
    {
        System.Type type = typeof(T);
        if (_dataDict.TryGetValue(type, out BaseEquipmentData data))
        {
            return data as T;
        }
        else
        {
            throw new System.Exception($"{type} is Null in Dict");
        }
    }
}

