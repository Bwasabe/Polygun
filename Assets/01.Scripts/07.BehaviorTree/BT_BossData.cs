using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BT_BossData : BT_Data
{
    [SerializeField]
    private float _speed;
    public float Speed => _speed;

    [SerializeField]
    private Transform _transform;

    public Transform Trm => _transform;
}
