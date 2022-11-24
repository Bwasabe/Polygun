using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static Yields;


public class Euclades_TeleportBullet : BT_Node
{
    public Euclades_TeleportBullet(BehaviorTree t, List<BT_Node> c = null) : base(t, c)
    {
        _data = _tree.GetData<Euclades_Data>();
        _target = GameManager.Instance.Player.transform;
        _portalCtrlPrefab = _tree.transform.Find("PortalController").GetComponent<TeleportBulletPortal>();
        _portalCtrlPrefab.gameObject.SetActive(false);

        for (int i = 0; i < 3; ++i)
        {
            Debug.Log(i + " : " + (i == 3));
        }
    }

    private Euclades_Data _data;

    private TeleportBulletPortal _portalCtrlPrefab;

    private int _currentPortalCount = 0;

    private Transform _target;

    private float _timer;

    protected override void OnEnter()
    {
        base.OnEnter();
        _currentPortalCount = 0;


    }

    protected override void OnUpdate()
    {
        NodeResult = Result.RUNNING;
        _timer += Time.deltaTime;
        if (_timer >= _data.PortalSpawnTimeList[_currentPortalCount])
        {
            _timer = 0f;


            Type type = this.GetType();
            var method = type.GetMethod($"PortalPage{_currentPortalCount}", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(this, null);

            Debug.Log(_data.PortalSpawnTimeList.Count);
            _currentPortalCount++;
            if (_currentPortalCount == _data.PortalSpawnTimeList.Count)
            {
                NodeResult = Result.SUCCESS;
                UpdateState = UpdateState.Exit;
                _tree.IsStop = true;
                return;
            }
        }
    }

    protected override void OnExit()
    {
        base.OnExit();
        _currentPortalCount = 0;
        _timer = 0f;
    }

    public override Result Execute()
    {
        base.Execute();
        return NodeResult;
    }

    private void PortalPage0()
    {
        SpawnPortal(_target.position, Quaternion.identity, 10f);
    }
    private void PortalPage1()
    {
        SpawnPortal(_target.position, Quaternion.Euler(0f, 90f, 0f), 10f);
    }

    private void PortalPage2()
    {
        _tree.StartCoroutine(SpawnHoriVertiPortal());
    }

    private IEnumerator SpawnHoriVertiPortal()
    {
        Vector3 pos = _target.position;
        for (int i = -(int)(_data.HorizontalPortalCount * 0.5f); i < _data.HorizontalPortalCount * 0.5f; ++i)
        {
            SpawnPortal(pos + Vector3.left * _data.PortalScale * i, Quaternion.identity, 20f);
            yield return WaitForSeconds(_data.HoriVertiPortalRate);
        }

        for (int i = -(int)(_data.VerticalPortalCount * 0.5f); i < _data.VerticalPortalCount * 0.5f; ++i)
        {
            SpawnPortal(pos + Vector3.forward * _data.PortalScale * i, Quaternion.Euler(0f, 90f, 0f), 20f, i + (int)(_data.VerticalPortalCount * 0.5f) == _data.VerticalPortalCount - 1);
            yield return WaitForSeconds(_data.HoriVertiPortalRate);
        }
    }

    private void SpawnPortal(Vector3 position, Quaternion rotation, float distance, bool isStop = false)
    {
        var g = GameObject.Instantiate(_portalCtrlPrefab, position, rotation, null);
        g.gameObject.SetActive(true);
        g.InitPortal(_tree);
        _tree.StartCoroutine(g.SpawnPortal(distance));
    }

}

public partial class Euclades_Data
{
    [SerializeField]
    private List<float> _portalSpawnTimeList = new List<float>();

    public List<float> PortalSpawnTimeList => _portalSpawnTimeList;

    [SerializeField]
    private int _horizontalPortalCount = 7;

    public int HorizontalPortalCount => _horizontalPortalCount;

    [SerializeField]
    private int _verticalPortalCount = 7;

    public int VerticalPortalCount => _verticalPortalCount;

    [SerializeField]
    private float _portalScale = 5f;

    public float PortalScale => _portalScale;

    [SerializeField]
    private float _portalScaleDuration = 0.8f;

    public float PortalScaleDuration => _portalScaleDuration;

    [SerializeField]
    private float _horiVertiPortalRate = 0.2f;

    public float HoriVertiPortalRate => _horiVertiPortalRate;
}
