using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static Yields;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CollisionCtrl))]
public class PlayerDamaged : BasePlayerComponent, IDmgAble
{
    [SerializeField]
    private float _invincibleTime = 0.4f;
    [SerializeField]
    private float _hitDuration = 0.3f;
    [SerializeField]
    private float _vignetteIntensity = 0.4f;
    [SerializeField]
    private int _invincibleCount = 2;
    [SerializeField]
    private HpText _hpText;

    private Color _deadVignetteColor;
    [SerializeField]
    private float _deadColorDuration = 0.2f;

    private MeshRenderer _model;

    private Coroutine _coroutine;

    private Vignette _vignette;

    protected override void Start()
    {
        base.Start();
        _model = transform.Find("Model").GetComponent<MeshRenderer>();
        if (!GameManager.Instance.GlobalVolume.profile.TryGet(out _vignette)) throw new System.Exception("Vignette is None or Volume is None");

        if (_model == null)
        {
            Debug.LogWarning("Transform FindName is Wrong or MeshRenderer is None");
        }
        // _player.PlayerStat.Init();
    }
    public void Damage(float damage)
    {
        if (_player.CurrentState.HasFlag(PLAYER_STATE.INVINCIBLE))
            return;

        _player.PlayerStat.Damaged(damage);
        _hpText.SetHp();

		if (_player.PlayerStat.HP <= 0)
        {
            Die();
        }
        else
        {
            DOTween.To(
                () => _vignette.intensity.value,
                value => _vignette.intensity.Override(value),
                _vignetteIntensity, _hitDuration * 0.5f
            ).SetLoops(2, LoopType.Yoyo);
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
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private void Die()
    {
        _player.CurrentState = PLAYER_STATE.DIE;
        Time.timeScale = 0.1f;
        DOTween.To(
                () => _vignette.intensity.value,
                value => _vignette.intensity.Override(value),
                _vignetteIntensity, _deadColorDuration
        ).SetLoops(2, LoopType.Yoyo).OnComplete(()=>{
            Time.timeScale = 1f;
            SceneManager.LoadScene("Action");
        });
        this.gameObject.SetActive(false);
    }

}
