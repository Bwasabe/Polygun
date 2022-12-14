using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UIElements;

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

    private Vector3 backPosition;
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
        nextPostion = _tree.transform.localPosition + (new Vector3(_thisData.maxMoveDistance, 0, 0) * min);
		moveCount = CurrentTime = 0;
        base.OnEnter();
    }

    private bool IsGround()
    {
        return Physics.Raycast(this._tree.transform.position,Vector3.down, 0.2f, _thisData.groundLayer);
    }
    private float y;
    protected override void OnUpdate()
    {
        if (Vector3.Distance(_tree.transform.position, _target.transform.position) > _thisData.maxMoveDistance)
            backPosition = _target.transform.position - _tree.transform.position;
        else
            backPosition = _tree.transform.position - _target.transform.position;

		nextPostion += backPosition.normalized;

		if (!IsGround())
        {
            RaycastHit ray;
            Physics.Raycast(this._tree.transform.position, Vector3.down, out ray, 100f, _thisData.groundLayer);
            Vector3 vec = ray.point;
			y = vec.y + _thisData.maxY;
        }

        ch.Move(nextPostion.normalized * Time.deltaTime * _thisData.Stat.Speed);
        _tree.transform.position = new Vector3(_tree.transform.position.x, y, _tree.transform.position.z);
		_tree.transform.LookAt(_target);
        CurrentTime += Time.deltaTime;
        UpdateState = UpdateState.Update;

		if (CurrentTime >= _thisData.waitMovingTime && moveCount < _thisData.maxMoveCount)
        {
            moveCount++;
			min *= -1;
            nextPostion = _tree.transform.localPosition + (new Vector3(_thisData.maxMoveDistance, 0, _thisData.maxMoveDistance) * min);
            CurrentTime = 0;
		}
        else if(CurrentTime >= _thisData.waitMovingTime && moveCount == _thisData.maxMoveCount)
        {
            _treeInfo.IsAttack = true;
			UpdateState = UpdateState.Exit;
        }
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
