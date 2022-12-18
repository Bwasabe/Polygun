using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefaultAttack : BaseSkill, ISkillInitAble
{
    private PlayerAttack _attack;
    public PlayerDefaultAttack(object parent) : base(null)
    {
        _attack = parent as PlayerAttack;
    }

    public override void Skill()
    {
        GameObject obj = ObjectPool.Instance.GetObject(PoolObjectType.PlayerBullet);
        obj.transform.position = _attack.AttackPos.position;
        obj.transform.rotation = _attack.transform.localRotation;
        Bullet bulletObj = obj.GetComponent<Bullet>();
        bulletObj.Direction = _attack.AttackPos.forward;
        bulletObj.Damage = _attack.Damage;
        bulletObj.HitLayer = _attack.HitLayer;
        bulletObj.Speed = _attack.BulletSpeed;
        _attack.ReloadCount--;
        // TODO: UI 업데이트
        if(_attack.ReloadCount == 0)
        {
            // 원이 돌아가면서 차오르는 UI 코루틴 실행시켜주기
            // 이거 BaseAttackSkill로 빼는 것도 좋을 듯
            // 칼의 경우 ReloadCount를 -1로 바꿔주기
        }
        CameraManager.Instance.CameraShake();
	}

    public void SkillInit()
    {
        _attack.ReloadCount = 20;
    }
}
