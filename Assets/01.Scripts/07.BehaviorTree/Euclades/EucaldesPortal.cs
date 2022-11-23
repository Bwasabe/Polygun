using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EucaldesPortal : MonoBehaviour
{
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private float _laserScaleDuration = 0.1f;
    [SerializeField]
    private float _laserWarringInterval;
    [SerializeField]
    private float _laserInterval = 0.2f;
    [SerializeField]
    private float _laserWarringScale = 1f;
    [SerializeField]
    private float _laserScale = 3f;
    [SerializeField]
    private float _laserDuration = 3f;

    private Collider _laserCollider;

    private void Awake() {
        _laserCollider = _laser.GetComponentInChildren<Collider>();
    }

    public void Lazer()
    {
        Vector3 scale = _laser.transform.localScale;
        scale.x = 0.01f;
        scale.y = 0.01f;
        _laser.transform.localScale = scale;
        // _laser.transform.localScale = Vector3.one * 0.01f;
        Sequence sequence = DOTween.Sequence()
        .AppendInterval(_laserWarringInterval)
        .Append(_laser.transform.DOScaleX(_laserWarringScale, _laserScaleDuration).SetLoops(2, LoopType.Yoyo))
        .Join(_laser.transform.DOScaleY(_laserWarringScale, _laserScaleDuration).SetLoops(2, LoopType.Yoyo))
        .AppendInterval(_laserInterval)
        .Append(_laser.transform.DOScaleX(_laserScale, _laserScaleDuration))
        .Join(_laser.transform.DOScaleY(_laserScale, _laserScaleDuration))
        .AppendInterval(_laserDuration)
        .Append(_laser.transform.DOScaleX(0f, _laserScaleDuration))
        .Join(_laser.transform.DOScaleY(0f, _laserScaleDuration))
        .AppendCallback(() => _laserCollider.enabled = false);
    }

    public void SetScale(float scaleZ)
    {
        Vector3 scale = _laser.transform.localScale;
        scale.z = scaleZ;

        _laser.transform.localScale = scale;
    }
}
