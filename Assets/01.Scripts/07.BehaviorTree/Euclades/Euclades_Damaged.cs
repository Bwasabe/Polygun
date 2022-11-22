using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Euclades_Damaged : MonoBehaviour, IDmgAble
{
    private Euclades _tree;

    private UnitStat _stat;

    private Material _material;
    private void Awake() {
        _stat = _tree.GetData<Euclades_Data>().Stat;
        _material = GetComponent<MeshRenderer>().material;
        _stat.Init();
    }

    public void Damage(int damage)
    {
        _stat.Damaged(damage);
        if (_stat.HP <= 0)
        {
            Die();
        }
        else
        {
            _material.DOColor(Color.white, 1f).SetLoops(2, LoopType.Yoyo);
        }
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }
}
