using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Euclades_TeleportBullet : BT_Node
{
    public Euclades_TeleportBullet(BehaviorTree t, List<BT_Node> c= null) : base(t,c)
    {
        _data = _tree.GetData<Euclades_Data>();
        //_portalCtrl = _tree.transform.Find("PortalCtrl").GetComponent<TeleportBulletPortal>();
    }

    private Euclades_Data _data;

    private TeleportBulletPortal _portalCtrl;

    protected override void OnEnter()
    {
        base.OnEnter();

        // _portalCtrl.SpawnPortal();

    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        
    }

    protected override void OnExit()
    {
        base.OnExit();
    }

    public override Result Execute()
    {
        base.Execute();
        return NodeResult;
    }

    private void BulletPage1()
    {

    }
    
    // public override Result Execute()
    // {
    //     _timer += Time.deltaTime;
    //     if (_currentBulletCount == 0)
    //     {
    //         //TODO: 조준 애니메이션 혹은 트윈들
    //         if (_timer >= _data.FirstBulletDelay)
    //         {
    //             //TODO: 총 쏘기
    //             _currentBulletCount++;
    //         }
    //         return Result.RUNNING;
    //     }
    //     else if(_currentBulletCount == _data.BulletCount)
    //     {
    //         return Result.SUCCESS;
    //     }
    //     else
    //     {
    //         if (_timer >= _data.BulletDelay)
    //         {
    //             //TODO: 총 쏘기
    //             _currentBulletCount++;
    //         }
    //         return Result.RUNNING;
    //     }
    // }
}

public partial class Euclades_Data
{
    [SerializeField]
    private int _bulletCount;

    public int BulletCount => _bulletCount;

    [SerializeField]
    private float _bulletDelay;
    public float BulletDelay => _bulletDelay;

    [SerializeField]
    private float _firstBulletDelay;
    public float FirstBulletDelay => _firstBulletDelay;

    [SerializeField]
    private float _portalScale = 3f;

    public float PortalScale => _portalScale;
}
