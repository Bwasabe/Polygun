using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class LongEnemyMove : BT_Node
{
    private LongEnemyData _thisData;

    private LongEnemy _treeInfo;
	private CharacterController ch;

	private float moveCount = 0f;

    private float CurrentTime = 0f;

    private Vector3 nextPostion = Vector3.zero;

    private int min = 1;

    private Transform _target;
	public LongEnemyMove(BehaviorTree t, Transform target,LongEnemyData longEnemyData,List<BT_Node> c = null) : base(t, c)
    {
        _thisData = longEnemyData;
        _treeInfo = _tree as LongEnemy;

		_tree.transform.localPosition -= new Vector3(_thisData.maxMoveDistance, 0, 0);
        _target = target;
        moveCount = 0;
		ch = _tree.GetComponent<CharacterController>();
	}

    protected override void OnEnter()
    {
        nextPostion = _tree.transform.localPosition + (new Vector3(_thisData.maxMoveDistance, _thisData.maxY, 0) * min);
		moveCount = CurrentTime = 0;
        base.OnEnter();
    }

    protected override void OnUpdate()
    {
        Vector3 back = -_tree.transform.forward;
        back *= 10;
		_tree.transform.localPosition = Vector3.Lerp(_tree.transform.localPosition, nextPostion, Time.deltaTime);
        _tree.transform.LookAt(_target);
        CurrentTime += Time.deltaTime;
        UpdateState = UpdateState.Update;
		if (CurrentTime >= _thisData.waitMovingTime && moveCount < _thisData.maxMoveCount)
        {
            moveCount++;
			min *= -1;
            nextPostion = _tree.transform.localPosition + (new Vector3(_thisData.maxMoveDistance, _thisData.maxY, 0) * min);
            CurrentTime = 0;
		}
        else if(CurrentTime >= _thisData.waitMovingTime && moveCount == _thisData.maxMoveCount)
        {
            _treeInfo.IsAttack = true;
			UpdateState = UpdateState.Exit;
        }
        base.OnUpdate();
    }

    protected override void OnExit()
    {
		if (moveCount == _thisData.maxMoveCount)
			NodeResult = Result.FAILURE;
		else if (moveCount < _thisData.maxMoveCount)
			NodeResult = Result.RUNNING;
		else
			NodeResult = Result.SUCCESS;
		base.OnExit();
    }

    public override Result Execute()
	{
		Debug.Log("MOVE");
		base.Execute();
        return NodeResult;
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
    [SerializeField]
    private float MaxY;
    [SerializeField]
    private float MaxZ;

    [SerializeField]
    private LayerMask _groundLayer;

    public LayerMask groundLayer => _groundLayer;
	public float maxZ => MaxZ;
    public float maxY => MaxY;
    public float waitMovingTime => WaitMovingTime;
    public float maxMoveDistance => MaxMoveDistance;

	public int maxMoveCount => MaxMoveCount;
}
