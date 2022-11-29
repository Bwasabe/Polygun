using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStat : UnitStat
{
    [SerializeField]
    private float _defaultSubSkillRatio;

    private float _subSkillRatio;
    public float SubSkillRatio => _subSkillRatio;
    public override void Init()
    {
        base.Init();
        ResetSubSkillRatio();
    }

    public void ResetSubSkillRatio()
    {
        _subSkillRatio = _defaultSubSkillRatio;
    }
}
