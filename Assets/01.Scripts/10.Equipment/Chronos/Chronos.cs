using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Chronos : BaseEquipment
{
    [SerializeField]
    private ChronosData _data;
    private Transform _watchCover;

    protected override void Awake()
    {
        base.Awake();
        _watchCover = transform.Find("Clock/WatchCover");

    }
    protected override void RegisterSkills()
    {
        _subSkill = new ChronosSubSkill(this);
        _attack = new ChronosAttack(this);
    }

    public override void PurchaseCallBack()
    {
        _watchCover.rotation = Quaternion.Euler(_data.WatchCoverRotation, 0f, 0f);

        base.PurchaseCallBack();
    }
}
