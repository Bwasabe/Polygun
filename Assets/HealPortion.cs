using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPortion : Item, IPurchaseAble
{
    [SerializeField]
    private float _hp;
    [SerializeField]
    private GameObject _returnObject;

    public bool IsShopPotion { get; set; } = false;

    public void PurchaseCallBack()
    {
        Player player = GameManager.Instance.Player;
        if (player.PlayerStat.HP < 100)
        {
            player.GetComponent<PlayerDamaged>().Damage(-_hp);
        }
        ObjectPool.Instance.ReturnObject(PoolObjectType.MiddleHeal, _returnObject);
    }
    protected override bool IsInteraction(Collider other)
    {
        return !IsShopPotion && base.IsInteraction(other);
    }

    protected override void Interaction(Collider other)
    {
        if (IsInteraction(other))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player.PlayerStat.HP < 100)
            {
                player.GetComponent<PlayerDamaged>().Damage(-_hp);
                ObjectPool.Instance.ReturnObject(PoolObjectType.MiddleHeal, _returnObject);
            }
        }
    }
}
