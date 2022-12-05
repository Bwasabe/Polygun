using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyDamaged : BaseEnemyDamaged
{
    private TestEnemy _testEnemy;

    protected override void Start()
    {
        _testEnemy = GetComponent<TestEnemy>();
        base.Start();
    }
    protected override void RegisterStat()
    {
        _stat = _testEnemy._unitStat;
    }
}
