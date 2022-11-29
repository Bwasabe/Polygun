using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefaultAttack : BaseSkill
{
    private PlayerAttack _parent;
    public PlayerDefaultAttack(object parent, SKillType type) : base(type)
    {
        _parent = parent as PlayerAttack;
    }

    public override void Skill()
    {
        GameObject obj = ObjectPool.Instance.GetObject(PoolObjectType.PlayerBullet);
        obj.transform.position = _parent.AttackPos.position;
        obj.transform.rotation = _parent.transform.localRotation;
        Bullet bulletObj = obj.GetComponent<Bullet>();
        bulletObj.Direction = _parent.AttackPos.forward;
        bulletObj.Damage = _parent.Damage;
        bulletObj.HitLayer = _parent.HitLayer;
        bulletObj.Speed = _parent.BulletSpeed;
        CameraManager.Instance.CameraShake();
	}
}
