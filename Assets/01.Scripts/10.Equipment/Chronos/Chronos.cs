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
    }
    protected override void RegisterSkills()
    {
        _subSkill = new ChronosSubSkill(this);
        _attack = new ChronosAttack(this);
    }
}
