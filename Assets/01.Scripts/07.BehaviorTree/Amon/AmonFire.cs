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

    [SerializeField]
    private PoolObjectType _objectType;

    private float _timer;

    private void Awake()
    {
        _collisionCtrl = GetComponent<CollisionCtrl>();
    }

    private void Start()
    {
        _collisionCtrl.ColliderStayEvent += FireDamage;
    }

    private void OnDisable()
    {
        _timer = 0f;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > Duration)
        {
            ObjectPool.Instance.ReturnObject(_objectType, gameObject);
        }
    }

    private void FireDamage(Collider other)
    {
        if (((1 << other.gameObject.layer) & _hitLayer) > 0)
        {
            other.GetComponent<IDmgAble>()?.Damage(damage);
        }
    }
}
