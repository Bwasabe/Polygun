using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmonProjectileBullet : Bullet
{
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private GameObject _firePrefab; // 애초에 가로 세로로 긴 녀석으로 만들기

    protected override void Hit(Collision other)
    {
        base.Hit(other);

        if(((1 << other.gameObject.layer) & _groundLayer) > 0)
        {
            // TODO: 이상하면 여기서 각도 바꿔주기
            GameObject g = GameObject.Instantiate(_firePrefab, other.contacts[0].point, transform.rotation);
            g.SetActive(true);
        }
    }


}
