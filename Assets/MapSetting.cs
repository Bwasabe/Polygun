using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
	Enter,
	Play,
	End
}
public class MapSetting : MonoBehaviour
{
	[SerializeField]
	private Vector3[] doorVec;

	private Map map;

	private bool isEnd;
	private bool isEnter;
	private MapState mapState;
	protected MapState MapState { get { return mapState; } set { if (MapState.End == value) isEnd = true; else mapState = value; } }

	private void Start()
	{
		map = GetComponentInParent<Map>();
		map.doorVec = doorVec;
		OnStart();
	}

	private void Update()
	{
		if (!isEnd && this.gameObject.transform.childCount > 2 && !isEnter)
		{
			OnEnter();
			isEnter = true;
		}

		if (isEnter && !isEnd)
			OnPlay();
	}
	protected virtual void OnStart()
	{	

	}
	protected virtual void OnEnter()
	{
	}

	protected virtual void OnPlay()
	{

	}
}
