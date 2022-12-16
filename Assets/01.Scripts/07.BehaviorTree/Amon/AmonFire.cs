using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionCtrl))]
public class AmonFire : MonoBehaviour
{
    [SerializeField]
    private LayerMask _hitLayer;

    private CollisionCtrl _collisionCtrl;

    [SerializeField]
    private float damage;

    private void Awake()
    {
        _collisionCtrl = GetComponent<CollisionCtrl>();

    }

    private void Start()
    {
        _collisionCtrl.ColliderStayEvent += FireDamage;
    }

    private void FireDamage(Collider other)
    {
        if (((1 << other.gameObject.layer) & _hitLayer) > 0)
        {
            if(!GameManager.Instance.Player.CurrentState.HasFlag(PLAYER_STATE.INVINCIBLE))
            {
                other.GetComponent<IDmgAble>()?.Damage(damage);
            }
        }
    }
}
