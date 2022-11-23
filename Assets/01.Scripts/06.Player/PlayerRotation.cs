using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerRotation : BasePlayerComponent
{
    // [SerializeField]
    // private LayerMask _groundLayer;

    [SerializeField]
    private float _rotateSmooth = 8f;

    private void Update()
    {
        Debug.Log("로테이션 : " + _player.CurrentState.HasFlag(PLAYER_STATE.ATTACK));
        if(_player.CurrentState.HasFlag(PLAYER_STATE.ATTACK))
        {
            Vector3 camRotation = MainCam.transform.eulerAngles;
            camRotation.x = 0f;
            camRotation.z = 0f;
            Debug.Log("공격이 왜 안돌지");
            transform.rotation = Quaternion.Slerp(transform.rotation, MainCam.transform.rotation, Time.deltaTime * _rotateSmooth);
        }
        // if (Physics.Raycast(MainCam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _groundLayer))
        // {
        //     Vector3 dir = (hit.point - transform.position).normalized;
        //     dir.y = 0f;
        //     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.2f);
        //     //Quaternion.LookRotation(dir * Time.deltaTime);  
        // }
    }

}
