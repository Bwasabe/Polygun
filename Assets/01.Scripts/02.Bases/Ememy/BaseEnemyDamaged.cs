using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseEnemyDamaged : MonoBehaviour, IDmgAble
{
    [SerializeField]
    private float _hitDuration = 0.15f;
    [SerializeField]
    private Color _hitColor = Color.white;
    [SerializeField]
    private GameObject CoinObject;
    [SerializeField]
    private float CoinPercent;

    protected UnitStat _stat;
    private Material _meshMaterial;
    private Color _defaultColor;

    private Tweener _tweener;
    protected virtual void Awake()
    {
        _meshMaterial = GetComponent<MeshRenderer>().material;
    }

    protected virtual void Start()
    {
        _defaultColor = _meshMaterial.color;
        RegisterStat();
        _stat.Init();
    }

    protected virtual void RegisterStat()
    {
        _stat = GetComponent<BehaviorTree>().GetData<BT_Data>().Stat;
    }

    public virtual void Damage(float damage)
    {
        _stat.Damaged(damage);
        if (_stat.HP <= 0)
        {
            Die();
        }
        else
        {
            if (_tweener != null)
            {
                _tweener.Kill();
                _meshMaterial.color = _defaultColor;
            }
            _tweener = _meshMaterial.DOColor(_hitColor, _hitDuration).SetLoops(2, LoopType.Yoyo);
        }
    }

    protected virtual void Die()
    {
        // TODO: 사라지는 애니메이션
        float rand = Random.Range(0, 100);
        if(CoinPercent <= rand)
        {
            GameObject obj = ObjectPool.Instance.GetObject(PoolObjectType.Coin);
			obj.transform.position = this.transform.position;
			obj.GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);
		}
        this.gameObject.SetActive(false);
	}

}