using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionCtrl))]
public class ShopGround : MonoBehaviour
{
    [SerializeField]
    private LayerMask _playerLayerMask;
    [SerializeField]
    private CollisionCtrl _collistionCtrl;
    private void Start()
    {
        _collistionCtrl.ColliderEnterEvent += GroundOut;

	}
    private void GroundOut(Collider col)
    {
        if (((1 << col.gameObject.layer) & _playerLayerMask) > 0)
        {
            CharacterController cc = col.GetComponent<CharacterController>();
            cc.enabled = false;
            col.transform.localPosition = new Vector3(121.6f, 0, -13.4f);
            col.GetComponent<IDmgAble>().Damage(5f);
            cc.enabled = true;
        }
    }
}
