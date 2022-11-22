using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class Euclades_Data : BT_Data
{

    [Header("스텟")]
    [SerializeField]
    private UnitStat _stat;

    public UnitStat Stat => _stat;
}
