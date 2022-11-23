using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[DisallowMultipleComponent]
[RequireComponent(typeof(CollisionCtrl))]
[RequireComponent(typeof(Euclades))]
public class Euclades_Damaged : MonoBehaviour, IDmgAble
{
    [SerializeField]
    private float _hitDuration = 0.15f;

    private Euclades _tree;
    private UnitStat _stat;
    private Material _material;
    private Color _defaultColor;

    private Tweener _tweener;
    private void Awake()
    {
        _tree = GetComponent<Euclades>();
        _material = GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        _defaultColor = _material.color;
        _stat = _tree.GetData<Euclades_Data>().Stat;
    }

    public void Damage(float damage)
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
            _tweener = _material.DOColor(Color.white, _hitDuration).SetLoops(2, LoopType.Yoyo);
        }
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }
}
