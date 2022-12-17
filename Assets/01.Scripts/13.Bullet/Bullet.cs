using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Yields;

public enum BulletType : int
{
    NONE = -1,
    ENEMY,
    PLAYER
}

[RequireComponent(typeof(CollisionCtrl))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private PoolObjectType _type = PoolObjectType.PlayerBullet;

    [SerializeField]
    private GameObject _flash;
    [SerializeField]
    private bool _isReturnObject = true;
    public float Damage { get; set; }
    public Vector3 Direction { get; set; }
    public float Speed { get; set; } = 1f;

    public LayerMask HitLayer { get; set; }

    public bool IsPlayerBullet { get; set; } = false;

    // [SerializeField]
    // private ParticleSystem _flush;
    // [SerializeField]
    // private ParticleSystem _hit;

    private ParticleSystem _particleSystem;

    private CollisionCtrl _collisionCtrl;


    private GameObject _flashObj;

    protected virtual void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _collisionCtrl = GetComponent<CollisionCtrl>();
        _collisionCtrl.CollisionEnterEvent += Hit;
    }

    protected virtual void Start()
    {
        if (_flashObj != null)
            DoFlash();
        else if (_flash != null)
        {
            _flashObj = Instantiate(_flash, transform);
            DoFlash();
        }
    }
    protected virtual void OnEnable()
    {

    }

    protected virtual void Update()
    {
        if (IsPlayerBullet)
        {
            this.transform.position += Direction.normalized * Speed * Time.deltaTime * GameManager.PlayerTimeScale;
        }
        else
            this.transform.position += Direction.normalized * Speed * Time.deltaTime;
        if (_particleSystem == null) return;
        if (!_particleSystem.IsAlive())
        {
            if (_isReturnObject && gameObject.activeSelf)
                ObjectPool.Instance.ReturnObject(_type, this.gameObject);
        }
    }
    void DoFlash()
    {
        _flashObj.transform.forward = gameObject.transform.forward;
        var flashPs = _flashObj.GetComponent<ParticleSystem>();
        flashPs.Play();
    }

    protected virtual void Hit(Collision other)
    {
        if (((1 << other.gameObject.layer) & HitLayer) > 0)
        {
            other.transform.GetComponent<IDmgAble>()?.Damage(Damage);
            gameObject.SetActive(false);
            // GameObject obj = ObjectPool.Instance.GetObject(PoolObjectType.PopUpDamage);
            // obj.GetComponent<DamagePopUp>().DamageText((int)Damage, this.transform.position);
        }
        // if (_isReturnObject && gameObject.activeSelf)
        //     ObjectPool.Instance.ReturnObject(_type, this.gameObject);
    }
}
