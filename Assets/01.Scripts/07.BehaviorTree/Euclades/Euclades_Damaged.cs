using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[DisallowMultipleComponent]
[RequireComponent(typeof(Euclades))]
public class Euclades_Damaged : BaseEnemyDamaged, IDmgAble
{
    private Euclades _tree;

    protected override void RegisterStat()
    {
        _stat = _tree.GetData<Euclades_Data>().Stat;
    }
}
