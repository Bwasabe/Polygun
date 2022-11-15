using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Euclades_SideviewPlatformer : BT_Node
{
    public Euclades_SideviewPlatformer(BehaviorTree t) : base(t)
    {
    }

    public override Result Execute()
    {
        return Result.FAILURE;
    }
}
