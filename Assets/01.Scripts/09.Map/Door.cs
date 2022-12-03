using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [NonSerialized]
    public Map nextMap;

    private CollisionCtrl col;

    public LayerMask layer;

    public DoorDirection dir;

    private CharacterController _cc;

    //private WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();
    private void Awake()
    {
        col = GetComponentInChildren<CollisionCtrl>();
        col.ColliderEnterEvent += NextDoor;
    }

    private void Start()
    {
        _cc = GameManager.Instance.Player.GetComponent<CharacterController>();
    }

    private void NextDoor(Collider other)
    {
        if (((1 << other.gameObject.layer) & layer) > 0)
        {
            other.gameObject.transform.parent = nextMap.transform.GetChild(0).transform;
            Vector3 vec = nextMap.doorVec[DirectionToInt(dir)];
            if (dir == DoorDirection.Foword || dir == DoorDirection.Back)
            {
                int x = dir == DoorDirection.Foword ? 2 : -2;
                vec.x += x;
            }
            else
            {
                int z = dir == DoorDirection.Left ? -2 : 2;
                vec.z += z;
            }
            _cc.enabled = false;
            other.transform.localPosition = vec;
            _cc.enabled = true;
        }
    }

    private int DirectionToInt(DoorDirection doorDirection)
    {
        if (doorDirection == DoorDirection.Foword || doorDirection == DoorDirection.Back)
            return doorDirection == DoorDirection.Foword ? 1 : 0;
        else
            return doorDirection == DoorDirection.Left ? 3 : 2;
    }
}
