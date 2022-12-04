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

    private void Awake()
    {
        _player = GameManager.Instance.Player;
    }

    public void ChangePlayerSkill<T>(BaseSkill skill) where T : BasePlayerSkillComponent
    {
        _player.GetPlayerComponent<T>().SetPlayerSkill(skill);
    }

    public void AddPlayerSkill<T>(BaseSkill skill) where T : BasePlayerSkillComponent
    {
        _player.GetPlayerComponent<T>().SetPlayerSkill(skill);

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

    public void RemovePlayerSkill<T>(BaseSkill skill, bool isNullPlayerSKill = false) where T : BasePlayerSkillComponent
    {
        if(isNullPlayerSKill)
        {
            _player.GetPlayerComponent<T>().SetPlayerSkill(skill);
        }

        _playerSkills.Remove(skill);

        ISkillPersistAble persistAble = skill.GetInterface<ISkillPersistAble>();
        if (persistAble != null)
            _persistSkills.Remove(persistAble);

        ISkillDmgAble dmgAble = skill.GetInterface<ISkillDmgAble>();

        if (dmgAble != null)
            _dmgSkills.Remove(dmgAble);

        ISkillAtkAble atkAble = skill.GetInterface<ISkillAtkAble>();

        if (atkAble != null)
            _atkSkills.Remove(atkAble);
    }
}
