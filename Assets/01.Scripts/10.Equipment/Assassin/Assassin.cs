using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : BaseEquipment
{
    [SerializeField]
    private AssassinData _data;
    protected AssassinSubSkill _subSkill;
    protected AssassinAttack _attack;

    public override BaseSkill GetAttack()
    {
        return _attack;
    }
    
    public override BaseSkill GetSubSkill()
    {
        return _subSkill;
    }

    public override void GetSkill()
    {
        _skillCtrl.AddPlayerSkill<PlayerSubSkill>(_subSkill);
        _skillCtrl.AddPlayerSkill<PlayerAttack>(_attack);
    }


    protected override void RegisterSkills()
    {
        _attack = new AssassinAttack(this);
        _subSkill = new AssassinSubSkill(this);
    }
}
