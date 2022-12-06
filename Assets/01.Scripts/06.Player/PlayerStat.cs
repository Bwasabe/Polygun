using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStat : UnitStat
{
    [SerializeField]
    private float _defaultSubSkillRatio;

    private float _subSkillRatio;
    public float SubSkillRatio { get => _subSkillRatio; set => _subSkillRatio = value; }

    public void ResetSubSkillRatio()
    {
        _subSkillRatio = _defaultSubSkillRatio;
    }
}
