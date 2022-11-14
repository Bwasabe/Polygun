using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Euclades_TeleportBullet : BT_Node
{
    public Euclades_TeleportBullet(BehaviorTree t) : base(t)
    {
        _data = _tree.GetData<Euclades_Data>();
    }

    private Euclades_Data _data;
    private int _currentBulletCount = 0;

    private float _timer;
    public override Result Execute()
    {
        _timer += Time.deltaTime;
        if (_currentBulletCount == 0)
        {
            //TODO: 조준 애니메이션 혹은 트윈들
            if (_timer >= _data.FirstBulletDelay)
            {
                //TODO: 총 쏘기
                _currentBulletCount++;
            }
            return Result.RUNNING;
        }
        else if(_currentBulletCount == _data.BulletCount)
        {
            return Result.SUCCESS;
        }
        else
        {
            if (_timer >= _data.BulletDelay)
            {
                //TODO: 총 쏘기
                _currentBulletCount++;
            }
            return Result.RUNNING;
        }
    }
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


}
