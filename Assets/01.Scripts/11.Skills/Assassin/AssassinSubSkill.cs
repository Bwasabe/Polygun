using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Yields;

public class AssassinSubSkill : BaseSkill, ISkillPersistAble, ISkillInitAble
{
    private AssassinData _data;
    private PlayerStat _player;
    public AssassinSubSkill(BaseEquipment parent) : base(parent)
    {
        _data = _parent.GetData<AssassinData>();
        _player = GameManager.Instance.Player.PlayerStat;
    }

    public override void Skill()
    {
        Vector3 dir = Define.MainCam.transform.position - _parent.transform.position;
        dir *= -1f;
        dir.Normalize();
        _data.StarFall.gameObject.SetActive(true);
        _parent.StartCoroutine(StarFallActiveFalse());
        _data.StarFall.transform.forward = dir;
        _data.StarFall.SetScale(_data.SubSkillDistance);
        
        if(Physics.Raycast(_parent.transform.position, dir, out RaycastHit hit, _data.SubSkillDistance, _data.HitLayer))
        {
            // 까매졌을 때 적 뒤로 가고 풀림
            Debug.Log("맞음");
        }
        else
        {
            // 깜빡이기만 하고 끝
            Debug.Log("안 맞음");
        }
    }

    private IEnumerator StarFallActiveFalse()
    {
        yield return WaitForSeconds(_data.SubSkillDuration);
        _data.StarFall.gameObject.SetActive(false);
    }

    public void SkillInit()
    {
        _player.SubSkillRatio = _data.SubSkillCoolTime;
    }

    public void SkillPersist()
    {
        
    }
}

public partial class AssassinData
{
    [SerializeField]
    private StarFall _starFall;
    public StarFall StarFall => _starFall;

    [SerializeField]
    private float _subSkillDistance = 30f;
    public float SubSkillDistance => _subSkillDistance;

    [SerializeField]
    private float _subSkillDuration = 0.1f;
    public float SubSkillDuration => _subSkillDuration;

    [SerializeField]
    private float _subSkillCoolTime = 5f;
    public float SubSkillCoolTime => _subSkillCoolTime;
}
