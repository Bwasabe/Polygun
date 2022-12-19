using UnityEngine;

[System.Serializable]
public class BaseEquipmentData
{
    [SerializeField]
    private int _reloadCount = -1;
    public int ReloadCount => _reloadCount;
    [SerializeField]
    private float _reloadDuration = 0;
    public float ReloadDuration => _reloadDuration;
}
