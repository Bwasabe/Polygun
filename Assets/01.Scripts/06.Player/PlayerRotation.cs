using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerRotation : BasePlayerComponent
{
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private float _rotateSmooth = 8f;

    private void Update()
    {
        if (Physics.Raycast(MainCam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _groundLayer))
        {
            Vector3 dir = (hit.point - transform.position).normalized;
            dir.y = 0f;
            transform.rotation = Quaternion.LookRotation(dir*Time.deltaTime);
        }
    }

    protected override void RegisterInput()
    {

    }
}
