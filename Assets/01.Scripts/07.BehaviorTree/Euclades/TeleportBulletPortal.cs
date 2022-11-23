using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBulletPortal : MonoBehaviour
{
    private EucaldesPortal _portalPrefab;

    private void Start() {
        _portalPrefab = transform.Find("Portal").GetComponent<EucaldesPortal>();
        _portalPrefab.gameObject.SetActive(false);
    }

    // public void SpawnPortal(Vector3 spawnPos, )
    // {
        
    // }
}
