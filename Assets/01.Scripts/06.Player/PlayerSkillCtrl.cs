using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillCtrl : BasePlayerComponent
{
    private List<BaseSkill> _playerSkills = new List<BaseSkill>();

    private List<ISkillPersistAble> _persistSkills = new List<ISkillPersistAble>();

    private List<ISkillDmgAble> _dmgSkills = new List<ISkillDmgAble>();

    private List<ISkillAtkAble> _atkSkills = new List<ISkillAtkAble>();


    private void Update()
    {
        _persistSkills.ForEach(x => x.SkillPersist());
    }

    private void Awake() {
        _player = GameManager.Instance.Player;
    }

    public void AddPlayerSkill(BaseSkill skill)
    {
        switch (skill.SKillType)
        {
            case SKillType.MainAttack:
                _player.GetPlayerComponent<PlayerAttack>().SetPlayerSkill(skill);
                break;
            case SKillType.SubSkill:
                _player.GetPlayerComponent<PlayerSubSkill>().SetPlayerSkill(skill);
                break;
            default:
                Debug.LogError("SkillType is undefined");
                break;
        }

        _playerSkills.Add(skill);
        skill.GetInterface<ISkillInitAble>()?.SkillInit();

        ISkillPersistAble persistAble = skill.GetInterface<ISkillPersistAble>();
        if (persistAble != null)
            _persistSkills.Add(persistAble);

        ISkillDmgAble dmgAble = skill.GetInterface<ISkillDmgAble>();

        if (dmgAble != null)
            _dmgSkills.Add(dmgAble);

        ISkillAtkAble atkAble = skill.GetInterface<ISkillAtkAble>();

        if (atkAble != null)
            _atkSkills.Add(atkAble);

    }
}
