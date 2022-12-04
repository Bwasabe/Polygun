using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class LongEnemyMove : BT_Node
{
    private LongEnemyData _thisData;

    private float moveCount = 0f;

    private float CurrentTime = 0f;

    private Vector3 nextPostion = Vector3.zero;

    private int min = 1;
	public LongEnemyMove(BehaviorTree t, LongEnemyData longEnemyData,List<BT_Node> c = null) : base(t, c)
    {
        _tree.transform.position -= new Vector3(_thisData.maxMoveDistance, 0, 0);
        _thisData = longEnemyData;
        moveCount = 0;
	}

    protected override void OnEnter()
    {
        nextPostion = _tree.transform.position + (new Vector3(_thisData.maxMoveDistance, 0, 0) * min);
		moveCount = CurrentTime = 0;
        base.OnEnter();
    }

    protected override void OnUpdate()
    {
        _tree.transform.position = Vector3.Lerp(_tree.transform.position, nextPostion, Time.deltaTime);
        CurrentTime += Time.deltaTime;
        UpdateState = UpdateState.Update;
		if (CurrentTime >= _thisData.waitMovingTime && moveCount < _thisData.maxMoveCount)
        {
            moveCount++;
			min *= -1;
            nextPostion = _tree.transform.position + (new Vector3(_thisData.maxMoveDistance, 0, 0) * min);
            CurrentTime = 0;
		}
        else if(CurrentTime >= _thisData.waitMovingTime && moveCount == _thisData.maxMoveCount)
        {
            UpdateState = UpdateState.Exit;
        }
        base.OnUpdate();
    }

	public override Result Execute()
	{
        if (moveCount == _thisData.maxMoveCount)
            return Result.FAILURE;
        else if (moveCount < _thisData.maxMoveCount)
            return Result.RUNNING;
        else
            return Result.SUCCESS;
	}
}

public partial class LongEnemyData
{
    [SerializeField]
    private int MaxMoveCount;
    [SerializeField]
    private float WaitMovingTime;
    [SerializeField]
    private float MaxMoveDistance;

    public float waitMovingTime => WaitMovingTime;
    public float maxMoveDistance => MaxMoveDistance;

	public int maxMoveCount => MaxMoveCount;
}
