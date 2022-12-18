using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmonMeleeBullet : Bullet
{
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private AmonFire _firePrefab; // 애초에 가로 세로로 긴 녀석으로 만들기
    [SerializeField]
    private float _duration = 5f;

    protected override void Hit(Collision other)
    {
        base.Hit(other);
        if(((1 << other.gameObject.layer) & _groundLayer) > 0)
        {
            // TODO: 이상하면 여기서 각도 바꿔주기
            AmonFire g = GameObject.Instantiate(_firePrefab, other.contacts[0].point, 
            Quaternion.Euler(0f, transform.eulerAngles.y, 0f));
            g.gameObject.SetActive(true);
            g.Duration = _duration;
        }
    }


}
