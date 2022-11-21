using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sequence로 Charge가 될 때 까지 기다림(선딜)
public class Euclades_ReadyToCharge : BT_Node
{
    public Euclades_ReadyToCharge(BehaviorTree t, List<BT_Node> c= null) : base(t, c) {
        _data = _tree.GetData<Euclades_Data>();
    }

    private Euclades_Data _data;

    private float _timer = 0f;

    // public override Result Execute()
    // {
    //     if(_data.IsCharge)
    //         return Result.SUCCESS;
        
    //     _timer += Time.deltaTime;
    //     if(_timer >= _data.ChargeReadyDuration)
    //     {
    //         _timer = 0f;
    //         return Result.SUCCESS;
    //     }
    //     else
    //     {
    //         return Result.FAILURE;
    //     }
    // }


}

public partial class Euclades_Data
{

}
