using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseEnemyDamaged : MonoBehaviour, IDmgAble
{
    [SerializeField]
    private float _hitDuration = 0.15f;
    [SerializeField]
    private Color _hitColor = Color.white;

    protected UnitStat _stat;
    private Material _material;
    private Color _defaultColor;

    private Tweener _tweener;
    protected virtual void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    protected virtual void Start()
    {
        _defaultColor = _material.color;
        RegisterStat();
        _stat.Init();
    }

    protected virtual void RegisterStat()
    {
        _stat = GetComponent<UnitStat>();
    }

    public virtual void Damage(float damage)
    {
        _stat.Damaged(damage);
        if (_stat.HP <= 0)
        {
            Die();
        }
        else
        {
            if (_tweener != null)
            {
                _tweener.Kill();
                _material.color = _defaultColor;
            }
            _tweener = _material.DOColor(_hitColor, _hitDuration).SetLoops(2, LoopType.Yoyo);
        }
    }

    protected virtual void Die()
    {
        // TODO: 사라지는 애니메이션
        this.gameObject.SetActive(false);
    }

}
