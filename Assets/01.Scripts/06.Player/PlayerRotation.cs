using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerRotation : BasePlayerComponent
{
    // [SerializeField]
    // private LayerMask _groundLayer;

    // [SerializeField]
    // private float _rotateSmooth = 8f;

    private void Update()
    {
        if (_player.CurrentState.HasFlag(PLAYER_STATE.ATTACK))
        {
            ChangeRotate();
            //Quaternion.Slerp(transform.rotation, Quaternion.Euler(camRotation), Time.deltaTime * _rotateSmooth * GameManager.PlayerTimeScale);
        }
        // if (Physics.Raycast(MainCam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _groundLayer))
        // {
        //     Vector3 dir = (hit.point - transform.position).normalized;
        //     dir.y = 0f;
        //     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.2f);
        //     //Quaternion.LookRotation(dir * Time.deltaTime);  
        // }
    }

    public void ChangeRotate()
    {
        Vector3 camRotation = MainCam.transform.eulerAngles;
        camRotation.x = 0f;
        camRotation.z = 0f;
        transform.rotation = Quaternion.Euler(camRotation);
    }

}
