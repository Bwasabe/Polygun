using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinSubSkill : BaseSkill, ISkillPersistAble
{
    public AssassinSubSkill(BaseEquipment parent) : base(parent)
    {
    }

    public override void Skill()
    {
        Debug.Log("어쌔신 서브");
    }

    public void SkillPersist()
    {
        
    }
}
