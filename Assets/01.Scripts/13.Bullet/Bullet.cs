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
    [SerializeField]
    private bool _isTriggerBullet = false;
    [SerializeField]
    private AudioClip _hitSound;

    public float Damage { get; set; }
    public Vector3 Direction { get; set; }
    public float Speed { get; set; } = 1f;
    public bool IgnorePitch { get; set; } = false;

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
        if(_isTriggerBullet)
        _collisionCtrl.ColliderEnterEvent += TriggerHit;
        else
        _collisionCtrl.CollisionEnterEvent += Hit;
    }

    protected virtual void Start()
    {
        if (_flashObj != null)
            DoFlash();
        else if (_flash != null)
        {
            _flashObj = Instantiate(_flash);
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
        // TODO : ?????????
        if (((1 << other.gameObject.layer) & HitLayer) > 0)
        {
            other.transform.GetComponent<IDmgAble>()?.Damage(Damage);
            GameObject obj = ObjectPool.Instance.GetObject(PoolObjectType.PopUpDamage);
            obj.GetComponent<DamagePopUp>().DamageText((int)Damage, transform.position);
            if(IgnorePitch)
            {
                SoundManager.Instance.Play(AudioType.IgnorePitch, _hitSound);
            }
            else
            {
                SoundManager.Instance.Play(AudioType.SFX, _hitSound);
            }
        }
        if(_isReturnObject)
            ObjectPool.Instance.ReturnObject(_type, this.gameObject);
    }

    protected virtual void TriggerHit(Collider other)
    {
        // TODO : ?????????
        if (((1 << other.gameObject.layer) & HitLayer) > 0)
        {
            other.transform.GetComponent<IDmgAble>()?.Damage(Damage);
            GameObject obj = ObjectPool.Instance.GetObject(PoolObjectType.PopUpDamage);
            obj.GetComponent<DamagePopUp>().DamageText((int)Damage, transform.position);
            if(IgnorePitch)
            {
                SoundManager.Instance.Play(AudioType.IgnorePitch, _hitSound);
            }
            else
            {
                SoundManager.Instance.Play(AudioType.SFX, _hitSound);
            }
        }
        if(_isReturnObject)
            ObjectPool.Instance.ReturnObject(_type, this.gameObject);
    }
}
