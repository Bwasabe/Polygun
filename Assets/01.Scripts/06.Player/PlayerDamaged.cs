using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : BasePlayerComponent, IDmgAble
{
    protected override void Start()
    {
        base.Start();
        _player.PlayerStat.ResetHp();
    }
    public void Damage(int damage)
    {
        _player.PlayerStat.Damaged(damage);
        if (_player.PlayerStat.HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.Player.CurrentState = PLAYER_STATE.DIE;
        Debug.Log("플레이어 죽음");
        Debug.Break();
        this.gameObject.SetActive(false);
    }

}
