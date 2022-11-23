 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static Yields;

public class TeleportBulletPortal : MonoBehaviour
{
    [SerializeField]
    private float _portalScale = 2.5f;

    [SerializeField]
    private float _portalScaleDuration = 0.8f;
    private EucaldesPortal _portalPrefab;

    public List<EucaldesPortal> _portals = new List<EucaldesPortal>();
    private void Start() {
        _portalPrefab = transform.Find("Portal").GetComponent<EucaldesPortal>();
        _portalPrefab.gameObject.SetActive(false);

        for (int i = 0; i < 2; ++i)
        {
            var g = Instantiate(_portalPrefab, transform.position, Quaternion.identity, transform);
            g.gameObject.SetActive(true);
            _portals.Add(g);
        }
        Debug.Log(_portals.Count);
    }

    public IEnumerator SpawnPortal(float distance)
    {
        _portals.ForEach(x =>
        {
            Vector3 pos = x.transform.localPosition;
            pos.z = distance * 0.5f;
            x.transform.position = pos;

            x.SetScale(distance * 0.5f);
        });
        Debug.Log(_portals.Count);
        for (int i = 0; i < _portals.Count; ++i)
        {
            Debug.Log("실행");
            _portals[i].transform.DOScale(Vector3.one * _portalScale, _portalScaleDuration);
        }
        Debug.Log("포탈");
        yield return WaitForSeconds(_portalScaleDuration);
        _portals.ForEach(x =>
        {
            x.Lazer();
        });
    }

}
