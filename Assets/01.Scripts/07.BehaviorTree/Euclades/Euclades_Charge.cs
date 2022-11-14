using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReadyToCharge에서 넘어온 다음 돌진 동작
public class Euclades_Charge : BT_Node
{
    public Euclades_Charge(BehaviorTree t) : base(t)
    {
        _cc = _tree.GetComponent<CharacterController>();
        _data = _tree.GetData<Euclades_Data>();
    }

    private CharacterController _cc;
    private Euclades_Data _data;

    private float _timer;

    public override Result Execute()
    {
        _timer += Time.deltaTime;
        if(_timer <= _data.ChargeDuration)
        {
            _data.IsCharge = true;
            // 돌진
            // TODO : 돌진 넣고 방향 추가하고 등등
            // _cc.Move(_data.ChargeSpeed * Time.deltaTime);
            return Result.RUNNING;
        }
        else
        {
            _data.ResetRandom();
            _data.IsCharge = false;
            return Result.SUCCESS;
        }
    }
}

public partial class Euclades_Data
{
    [SerializeField]
    private float _chargeDuration;

    public float ChargeDuration {
        get => _chargeDuration;
        set => _chargeDuration = value;
    }

    [SerializeField]
    private float _chargeSpeed;

    public float ChargeSpeed
    {
        get => _chargeSpeed;
        set => _chargeSpeed = value;
    }
}
