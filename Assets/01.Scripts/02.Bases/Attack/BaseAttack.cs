using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttack
{
    protected BaseAttack()
    {}

    public abstract void Attack(float damage, LayerMask layer, float speed);
}
