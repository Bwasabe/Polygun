using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EucaldesPortal : MonoBehaviour
{
    private Transform _attackPosition;

    private void Start() {
        _attackPosition = transform.Find("AttackPosition");
    }

    private void Attack(int damage, LayerMask hitLayer, float speed)
    {
        GameObject obj = ObjectPool.Instance.GetObject(PoolObjectType.PlayerBullet);
        obj.transform.position = _attackPosition.position;
        obj.transform.rotation = this.transform.localRotation;
        Bullet bulletObj = obj.GetComponent<Bullet>();
        bulletObj.Direction = _attackPosition.forward;
        bulletObj.Damage = damage;
        bulletObj.HitLayer = hitLayer;
        bulletObj.Speed = speed;
    }
}
