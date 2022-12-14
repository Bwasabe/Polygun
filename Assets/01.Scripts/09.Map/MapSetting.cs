using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
	None,
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
	protected MapState MapState { get { return mapState; } set { if (MapState.End == value) { isEnd = true; OnExit(); } else mapState = value; } }

	private void Start()
	{
		map = GetComponentInParent<Map>();
		map.doorVec = doorVec;
		map.DoorCreates();
		OnStart();
	}

	protected virtual void Update()
	{
		if (!isEnd && !isEnter)
		{
			if (OnIsEnter())
			{
				OnEnter();
				isEnter = true;
			}
		}

		if (isEnter && !isEnd)
			OnPlay();
	}
	protected virtual void OnStart()
	{	

	}

    protected virtual void OnExit()
	{
		map.DoorOpen();
	}
    public virtual void RepeatOnEnter()
	{
		MiniMap.Instance.MiniMapSet(map);
	}
	protected virtual void OnEnter()
	{
		map.DoorLock();
	}

	protected virtual void OnPlay()
	{
	}

	protected virtual bool OnIsEnter()
	{
		return transform.Find("Player") != null;
	}
}
