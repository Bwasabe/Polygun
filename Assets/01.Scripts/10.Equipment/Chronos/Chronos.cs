using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chronos : BaseEquipment
{
    [SerializeField]
    private ChronosData _data;
    
    protected override void RegisterSkills()
    {
        _subSkill = new ChronosSubSkill(this);
        _attack = new ChronosAttack(this);
    }
}
