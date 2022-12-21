using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Yields;

[RequireComponent(typeof(CollisionCtrl))]
public class PlayerDamaged : BasePlayerComponent, IDmgAble
{
    [SerializeField]
    private float _invincibleTime = 0.4f;
    [SerializeField]
    private int _invincibleCount = 2;
    [SerializeField]
    private HpText _hpText;

    private MeshRenderer _model;

    private Coroutine _coroutine;

    protected override void Start()
    {
        base.Start();
        _model = transform.Find("Model").GetComponent<MeshRenderer>();
        if(_model == null)
        {
            Debug.LogWarning("Transform FindName is Wrong or MeshRenderer is None");
        }
        // _player.PlayerStat.Init();
    }
    public void Damage(float damage)
    {
        if(_player.CurrentState.HasFlag(PLAYER_STATE.INVINCIBLE))
            return;
        
        _player.PlayerStat.Damaged(damage);
        _hpText.SetHp();

		if (_player.PlayerStat.HP <= 0)
        {
            Die();
        }
        else
        {
            _player.CurrentState |= PLAYER_STATE.INVINCIBLE;
            _coroutine = StartCoroutine(InvinciblePlayer());
        }
    }

    private IEnumerator InvinciblePlayer()
    {
        for (int i = 0; i < _invincibleCount; ++i)
        {
            _model.gameObject.SetActive(false);
            yield return WaitForSeconds(_invincibleTime / (_invincibleCount * 2));
            _model.gameObject.SetActive(true);
            yield return WaitForSeconds(_invincibleTime / (_invincibleCount * 2));
        }
        _player.CurrentState &= ~PLAYER_STATE.INVINCIBLE;
    }

    public void StopInvinciblePlayer()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
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
