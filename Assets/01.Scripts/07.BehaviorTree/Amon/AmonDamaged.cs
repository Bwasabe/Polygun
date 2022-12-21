using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AmonDamaged : BaseEnemyDamaged
{
    private Amon _tree;
    private const string BASE_COLOR = "_BaseColor";
    protected override void Awake()
    {
        _meshMaterial = GetComponentInChildren<SkinnedMeshRenderer>().material;
        _tree = GetComponent<Amon>();
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void Die()
    {
        base.Die();
    }

    protected override void RegisterStat()
    {
        _stat = _tree.GetData<AmonData>().Stat;
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
    }

    protected override void InitDefaultColor()
    {
        _defaultColor = _meshMaterial.GetColor(BASE_COLOR);
    }

    protected override void SetDefaultColor()
    {
        _meshMaterial.SetColor(BASE_COLOR, _defaultColor);
    }

    protected override Tweener ChangeColor()
    {
        
        return DOTween.To(()=> _meshMaterial.GetColor(BASE_COLOR),
            color => _meshMaterial.SetColor(BASE_COLOR, color),
            _hitColor, _hitDuration).SetLoops(2, LoopType.Yoyo);
    }
}
