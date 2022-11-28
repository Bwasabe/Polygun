using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefaultAttack : BaseAttack
{
    private PlayerAttack _parent;
    public PlayerDefaultAttack(object parent) : base()
    {
        _parent = parent as PlayerAttack;
    }

    public override void Attack(float damage, LayerMask layer, float speed)
    {
        GameObject obj = ObjectPool.Instance.GetObject(PoolObjectType.PlayerBullet);
        obj.transform.position = _parent.AttackPos.position;
        obj.transform.rotation = _parent.transform.localRotation;
        Bullet bulletObj = obj.GetComponent<Bullet>();
        bulletObj.Direction = _parent.AttackPos.forward;
        bulletObj.Damage = damage;
        bulletObj.HitLayer = layer;
        bulletObj.Speed = speed;
        CameraManager.Instance.CameraShake();
	}
}
