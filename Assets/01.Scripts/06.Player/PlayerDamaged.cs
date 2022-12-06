using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionCtrl))]
public class PlayerDamaged : BasePlayerComponent, IDmgAble
{
    protected override void Start()
    {
        base.Start();
        // _player.PlayerStat.Init();
    }
    public void Damage(float damage)
    {
        if(_player.CurrentState.HasFlag(PLAYER_STATE.INVINCIBLE))
            return;
        _player.PlayerStat.Damaged(damage);
        Debug.Log(damage);
        if (_player.PlayerStat.HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _player.CurrentState = PLAYER_STATE.DIE;
        Debug.Log("플레이어 죽음");
        Debug.Break();
        this.gameObject.SetActive(false);
    }

}
