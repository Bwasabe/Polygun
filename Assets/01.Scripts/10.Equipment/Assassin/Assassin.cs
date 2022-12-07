using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : BaseEquipment
{
    [SerializeField]
    private AssassinData _data;

    protected override void RegisterSkills()
    {
        _attack = new AssassinAttack(this);
        _subSkill = new AssassinSubSkill(this);
    }
}
