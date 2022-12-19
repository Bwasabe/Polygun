using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Chronos : BaseEquipment
{
    [SerializeField]
    private ChronosData _data;
    protected override void Awake()
    {
        base.Awake();
        _data.GlobalVolume = GameObject.FindObjectOfType<Volume>();
        Debug.Log(_data.GlobalVolume);
    }
    protected override void RegisterSkills()
    {
        _subSkill = new ChronosSubSkill(this);
        _attack = new ChronosAttack(this);
    }
}
