using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamaged : MonoBehaviour, IDmgAble
{
    [SerializeField]
    private UnitStat _stat;
    public void Damage(int damage)
    {
        
    }
}
