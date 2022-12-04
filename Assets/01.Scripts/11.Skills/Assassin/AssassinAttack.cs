using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinAttack : BaseSkill, ISkillInitAble
{
    public AssassinAttack(BaseEquipment parent) : base(parent)
    {
        // _attackPrefab = _parent.GetData<AssassinData>().AttackPrefab;
    }

    private GameObject _attackPrefab;

    //TODO: 근접공격 오브젝트 생성
    public override void Skill()
    {
        Debug.Log("어쌔신 근접 공격");
        
    }

    // TODO: 여기서 공격 시간 조정해주기
    public void SkillInit()
    {
        
    }
}
