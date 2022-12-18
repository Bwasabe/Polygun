using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
	protected MapState MapState { get { return mapState; } set { if (MapState.End == value) { isEnd = true;  map.DoorOpen(); } else mapState = value; } }

	private void Start()
	{
		map = GetComponentInParent<Map>();
		map.doorVec = doorVec;
		map.DoorCreates();
		OnStart();
	}

	private void Update()
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
