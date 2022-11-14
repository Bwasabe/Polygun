using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class Euclades_Data : BT_Data
{
    [SerializeField]
    private int _hp;
    public int Hp => _hp;


    [SerializeField]
    private float _moveSpeed;
    public float MoveSpeed => _moveSpeed;
}
