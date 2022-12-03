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

	public GameObject[] roomObjects;
	public RoomType roomType;

	//[NonSerialized]
	public Vector3[] doorVec;

	[SerializeField]
	private GameObject wallObj;
	[SerializeField]
	private GameObject doorObj;

	private GameObject PObj;

	public void MapCreate()
	{
		GameObject obj = Instantiate(roomObjects[(int)roomType], transform);
		obj.transform.localPosition = Vector3.zero;
		PObj = obj.transform.GetChild(0).gameObject;
	}
	public void DoorCreates()
	{
		bool[] dirbools = new bool[4];
		foreach (Map map in moveMaps)
		{
			if (map.pos == new Vector2Int(pos.x + 1, pos.y))
			{
				dirbools[(int)DoorDirection.Foword] = true;
			}
			if (map.pos == new Vector2Int(pos.x, pos.y + 1))
			{
				dirbools[(int)DoorDirection.Right] = true;
			}
			if (map.pos == new Vector2Int(pos.x - 1, pos.y))
			{
				dirbools[(int)DoorDirection.Back] = true;
			}
			if (map.pos == new Vector2Int(pos.x, pos.y - 1))
			{
				dirbools[(int)DoorDirection.Left] = true;
			}
		}

		for (int i = 0; i < 4; i++)
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
		if ((DoorDirection)i == DoorDirection.Foword || (DoorDirection)i == DoorDirection.Back)
			obj.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
		obj.transform.localPosition = doorVec[i];
		Pair<int, int> pair = DirToPair((Direction)i);

		obj.GetComponent<Door>().nextMap = moveMaps.Find((x) => x.pos == new Vector2Int(pos.x + pair.first, pos.y + pair.secound));
		obj.GetComponent<Door>().dir = (DoorDirection)i;
	}
	private Pair<int, int> DirToPair(Direction dir) => (dir) switch
	{
		Direction.Foword => new Pair<int, int>() { first = 1, secound = 0 },
		Direction.Back => new Pair<int, int>() { first = -1, secound = 0 },
		Direction.Left => new Pair<int, int>() { first = 0, secound = -1 },
		Direction.Right => new Pair<int, int>() { first = 0, secound = 1 },
		_ => new Pair<int, int>() { first = 1, secound = 0 }
	};
	private void WallCreate(int i)
	{
		GameObject obj = Instantiate(wallObj, PObj.transform);
		if ((Direction)i == Direction.Foword || (Direction)i == Direction.Back)
			obj.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
		obj.transform.localPosition = doorVec[i];
	}
}
