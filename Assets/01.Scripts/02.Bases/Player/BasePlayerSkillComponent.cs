using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerSkillComponent : BasePlayerComponent
{
    protected BaseSkill _skill;

    public void SetPlayerSkill(BaseSkill skill)
    {
        _skill = skill;
    }
}
