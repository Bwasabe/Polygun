using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public enum DoorDirection
{
	Foword,
	Back,
	Left,
	Right,
}

public class Map : MonoBehaviour
{
	public Vector2Int pos;

	public List<Map> moveMaps;

	public bool isEnd;

	[SerializeField]
	private Vector3[] doorVec;

	[SerializeField]
	private GameObject wallObj;
	[SerializeField]
	private GameObject doorObj;

	[SerializeField]
	private GameObject PObj;
	public void DoorCreates()
	{
		bool[] dirbools = new bool[4]; 
		foreach(Map map in moveMaps)
		{
			if (map.pos == new Vector2Int(pos.x+1,pos.y))
			{
				dirbools[(int)DoorDirection.Foword] = true;
			}
			if (map.pos == new Vector2Int(pos.x, pos.y+1))
			{
				dirbools[(int)DoorDirection.Right] = true;
			}
			if(map.pos == new Vector2Int(pos.x - 1, pos.y))
			{
				dirbools[(int)DoorDirection.Back] = true;
			}
			if (map.pos == new Vector2Int(pos.x, pos.y - 1))
			{
				dirbools[(int)DoorDirection.Left] = true;
			}
		}

		for(int i =0; i<4; i++)
		{
			if (dirbools[i])
				DoorCreate(i);
			else
				WallCreate(i);
		}
	}

	private void DoorCreate(int i)
	{
		GameObject obj = Instantiate(doorObj, PObj.transform);
		if ((Direction)i == Direction.Foword || (Direction)i == Direction.Back)
			obj.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
		obj.transform.localPosition = doorVec[i];
	}

	private void WallCreate(int i)
	{
		GameObject obj = Instantiate(wallObj, PObj.transform);
		if ((Direction)i == Direction.Foword || (Direction)i == Direction.Back)
			obj.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
		obj.transform.localPosition = doorVec[i];
	}
}
