using UnityEngine;

[System.Serializable]
public struct Page
{
    [SerializeField]
    private float _maxHp;
    public float MaxHp => _maxHp;

    [SerializeField]
    private float _minHp;

    public float MinHp => _minHp;
}