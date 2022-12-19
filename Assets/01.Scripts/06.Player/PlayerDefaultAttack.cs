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
        CameraManager.Instance.CameraShake();
	}

    public void SkillInit()
    {
        _attack.SetReload(20, 1.5f);
    }
}
