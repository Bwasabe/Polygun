using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static Yields;

public class TeleportBulletPortal : MonoBehaviour
{

    private EucaldesPortal _portalPrefab;

    private List<EucaldesPortal> _portals = new List<EucaldesPortal>();
    private BehaviorTree _euclades;

    private Euclades_Data _data;
    private void Awake()
    {
        _portalPrefab = transform.Find("Portal").GetComponent<EucaldesPortal>();
        _portalPrefab.gameObject.SetActive(false);
    }
    public void InitPortal(BehaviorTree euclades)
    {
        _euclades = euclades;
        _data = _euclades.GetData<Euclades_Data>();
        for (int i = 0; i < 2; ++i)
        {
            var g = Instantiate(_portalPrefab, transform);
            g.gameObject.SetActive(true);
            _portals.Add(g);
        }
    }
    public IEnumerator SpawnPortal(float distance, bool isStop = false)
    {
        Vector3 pos = _portals[0].transform.localPosition;
        pos.z = distance * 0.5f;
        _portals[0].transform.localPosition = pos;

        Vector3 pos2 = _portals[1].transform.localPosition;
        pos2.z = -distance * 0.5f;
        _portals[1].transform.localPosition = pos2;

        _portals.ForEach(x =>
        {
            x.transform.LookAt(transform);
            x.SetScale(_data.PortalScale);
        });
        Debug.Log(_portals.Count);
        for (int i = 0; i < _portals.Count; ++i)
        {
            _portals[i].transform.DOScale(Vector3.one * _data.PortalScale, _data.PortalScaleDuration);
        }
        yield return WaitForSeconds(_data.PortalScaleDuration);
        _portals.ForEach(x =>
        {
            if(isStop)
            {
                Sequence s = x.Lazer().Append(x.transform.DOScale(Vector3.zero, _data.PortalScaleDuration)).AppendCallback(()=>_euclades.IsStop = false);
            }
            else
            {
                Sequence s = x.Lazer().Append(x.transform.DOScale(Vector3.zero, _data.PortalScaleDuration));
            }
        });
    }

}
