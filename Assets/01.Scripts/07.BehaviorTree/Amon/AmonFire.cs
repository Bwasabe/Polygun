using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Yields;

[RequireComponent(typeof(CollisionCtrl))]
public class AmonFire : MonoBehaviour
{
    [SerializeField]
    private LayerMask _hitLayer;
    [SerializeField]
    private float damage;

    private CollisionCtrl _collisionCtrl;
    public float Duration { get; set; } = 0f;

    private float _timer;

    private void Awake()
    {
        _collisionCtrl = GetComponent<CollisionCtrl>();
    }

    private void Start()
    {
        _collisionCtrl.ColliderStayEvent += FireDamage;
    }

    private void OnDisable() {
        _timer = 0f;
    }

    private void Update() {
        _timer += Time.deltaTime;
        if(_timer > Duration)
        {
            // TODO: 풀링
            gameObject.SetActive(false);
        }
    }

    private void FireDamage(Collider other)
    {
        if (((1 << other.gameObject.layer) & _hitLayer) > 0)
        {
            if(!GameManager.Instance.Player.CurrentState.HasFlag(PLAYER_STATE.INVINCIBLE))
            {
                other.GetComponent<IDmgAble>()?.Damage(damage);
            }
        }
    }
}
