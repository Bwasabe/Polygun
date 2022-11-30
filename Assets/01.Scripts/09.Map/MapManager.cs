using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum Direction
{
	Foword,
	Back,
	Left,
	Right
}

public enum RoomType
{
	StartRoom,
	NomalRoom,
	StoreRoom,
	EffectRoom,
	BossRoom,
}

[Serializable]
public struct Floor
{
	public int normalCount;
	public int storeCount;
	public int effectCount;

	public int mapMaxCreateCount => normalCount + storeCount + effectCount + 2;
}
public class MapManager : MonoBehaviour
{
	public GameObject[] normalObjs;

	public Floor[] floor;

	[SerializeField]
	private GameObject playerObj;


	private Floor CurrentFloor => floor[floorCount];
	private int floorCount = 0;
	private int mapMaxCreateCount => floor[floorCount].mapMaxCreateCount;
	private int mapCreateCount = 0;
	private bool[,] mapCreateArray; //맵 생성 된 곳 표시
	private bool[,] mapisSearchArray; //맵 dfs로 찾기용 변수
	private List<Vector2Int>[,] mapMoveArray;
	private Map[,] mapInfoArray; //map의 정보가 담겨있는 변수;
	private Queue<Pair<int, int>> pairQueue;
	private List<GameObject> mapCreateList ;

	private void Start()
	{
		MapInit();

		playerObj.transform.parent = mapInfoArray[mapMaxCreateCount / 2, mapMaxCreateCount / 2]
	.gameObject.transform;

		playerObj.transform.localPosition = new Vector3(-11, 0, 6);
	}
	public void MapInit()
	{
		mapCreateArray = new bool[mapMaxCreateCount, mapMaxCreateCount];
		mapisSearchArray = new bool[mapMaxCreateCount, mapMaxCreateCount];
		mapMoveArray = new List<Vector2Int>[mapCreateCount, mapCreateCount];
		mapInfoArray = new Map[mapMaxCreateCount, mapMaxCreateCount];
		pairQueue = new Queue<Pair<int, int>>();

		mapCreateCount = 0;


		MapCreateList();
		MapCreate(mapMaxCreateCount / 2, mapMaxCreateCount / 2);
		while (pairQueue.Count != 0 && mapCreateCount < mapMaxCreateCount)
		{
			Pair<int, int> pair = pairQueue.Dequeue();
			MapCreate(pair.first, pair.secound);
		}
		MapSearch(mapMaxCreateCount / 2, mapMaxCreateCount / 2, Vector2Int.zero);
		MapInstanceCreate();
		MapDoorSet();
	}

	#region 맵 생성하는 것
	private void MapCreateList()
	{
		for (int i = 0; i < CurrentFloor.normalCount; i++)
		{
			mapCreateList.Add(normalObjs[(int)RoomType.NomalRoom]);
		}
		
		for(int i =0; i < CurrentFloor.storeCount; i++)
		{
			mapCreateList.Add(normalObjs[(int)RoomType.StoreRoom]);
		}

		for (int i = 0; i < CurrentFloor.effectCount; i++)
		{
			mapCreateList.Add(normalObjs[(int)RoomType.EffectRoom]);
		}
		//mapCreateList.Add(normalObjs[(int)RoomType.StartRoom]);
		//mapCreateList.Add(normalObjs[(int)RoomType.BossRoom]);
	}
	private void MapCreate(int x, int y)
	{
		if (mapCreateCount >= mapMaxCreateCount)
		{
			return;
		}

		if (mapCreateArray[x, y])
			return;

		//GameObject obj = Instantiate(debugObj, transform);
		//obj.transform.position = new Vector3(x * 20, 0, y * 20);
		//mapInfoArray[x, y] = obj.GetComponent<Map>();
		//mapInfoArray[x, y].pos = new Vector2Int(x, y);
		mapCreateArray[x, y] = true;
		mapCreateCount++;

		List<Direction> dir = new List<Direction>();

		for (int i = 0; i < 4; i++)
		{
			dir.Add((Direction)i);
		}
		DirRemove(ref dir, x, y);

		int rand = UnityEngine.Random.Range(1, dir.Count);

		ShuffleArray(dir);

		for (int i = 0; i < rand; i++)
		{
			Pair<int, int> pair = DirToPair(dir[i]);
			pairQueue.Enqueue(new Pair<int, int>(x + pair.first, y + pair.secound));
		}
	}
	private void MapSearch(int x, int y, Vector2Int beforePos)
	{

		if (x < 0 || x >= mapMaxCreateCount
		|| y < 0 || y >= mapMaxCreateCount)
			return;

		if (!mapCreateArray[x, y])
		{
			return;
		}

		if (mapisSearchArray[x, y])
		{
			return;
		}

		mapisSearchArray[x, y] = true;

		if (x == mapMaxCreateCount / 2 && y == mapMaxCreateCount / 2)
		{
			mapInfoArray[x, y].name = "start";
		}

		if (beforePos != Vector2Int.zero)
		{
			if (mapMoveArray[x, y].Count != 0)
			{
				int Randoms = UnityEngine.Random.Range(0, 1);
				if (Randoms != 0)
				{
					mapMoveArray[x,y].Add(beforePos);
					mapMoveArray[beforePos.x,beforePos.y].Add(new Vector2Int(x,y));
				}
			}
			else
			{
				mapMoveArray[x, y].Add(beforePos);
				mapMoveArray[beforePos.x, beforePos.y].Add(new Vector2Int(x, y));
			}
		}


		Pair<int, int> pair = DirToPair(Direction.Foword);
		MapSearch(x + pair.first, y + pair.secound, new Vector2Int(x, y));
		pair = DirToPair(Direction.Back);
		MapSearch(x + pair.first, y + pair.secound, new Vector2Int(x, y));
		pair = DirToPair(Direction.Left);
		MapSearch(x + pair.first, y + pair.secound, new Vector2Int(x, y));
		pair = DirToPair(Direction.Right);
		MapSearch(x + pair.first, y + pair.secound, new Vector2Int(x, y));
	}
	private void MapInstanceCreate()
	{
		for(int i = 0; i<mapMaxCreateCount; i++)
		{
			for(int j = 0; j<mapMaxCreateCount; j++)
			{
				if (mapCreateArray[i,j])
				{
					if (mapMoveArray[i,j].Count > 1)
					{
						
					}
					else
					{
						EndMapCreate();
					}
				}
			}
		}
	}

	private void NormalMapCreate(int x, int y, RoomType roomType)
	{
		GameObject obj = Instantiate(normalObjs[(int)roomType], this.transform);
	}

	private void EndMapCreate()
	{

	}
	private void MapDoorSet()
	{
		for (int i = 0; i < mapMaxCreateCount; i++)
		{
			for (int j = 0; j < mapMaxCreateCount; j++)
			{
				if (mapCreateArray[i, j])
				{
					mapInfoArray[i, j].DoorCreates();

					//if(mapInfoArray[i, j].moveMaps.Count == 1 &&
					//	!(i == mapCreateCount /2 && j == mapCreateCount/2))
					//{
					//	EndMaps.Add(mapInfoArray[i, j]);
					//}
				}
			}
		}
	}
	#endregion

	#region 부가적인 것들
	private void DirRemove(ref List<Direction> dir, int x, int y)
	{
		if (x - 1 <= 0)
		{
			dir.Remove(Direction.Back);
		}
		else
		{
			if (mapCreateArray[x - 1, y])
			{
				dir.Remove(Direction.Back);
			}
		}

		if (y - 1 <= 0)
		{
			dir.Remove(Direction.Right);
		}
		else
		{
			if (mapCreateArray[x, y - 1])
			{
				dir.Remove(Direction.Right);
			}
		}

		if (x + 1 >= mapMaxCreateCount)
		{
			dir.Remove(Direction.Foword);
		}
		else
		{
			if (mapCreateArray[x + 1, y])
			{
				dir.Remove(Direction.Foword);
			}
		}

		if (y + 1 >= mapMaxCreateCount)
		{
			dir.Remove(Direction.Left);
		}
		else
		{
			if (mapCreateArray[x, y + 1])
			{
				dir.Remove(Direction.Left);
			}
		}
	}
	private Pair<int, int> DirToPair(Direction dir) => (dir) switch
	{
		Direction.Foword => new Pair<int, int>() { first = 1, secound = 0 },
		Direction.Back => new Pair<int, int>() { first = -1, secound = 0 },
		Direction.Left => new Pair<int, int>() { first = 0, secound = 1 },
		Direction.Right => new Pair<int, int>() { first = 0, secound = -1 },
		_ => new Pair<int, int>() { first = 1, secound = 0 }
	};
	private void ShuffleArray<T>(List<T> list)
	{
		for (int i = 0; i < 100; i++)
		{
			int firstindex = UnityEngine.Random.Range(0, list.Count - 1);
			int secoundindex = UnityEngine.Random.Range(0, list.Count - 1);

			T temp = list[firstindex];
			list[firstindex] = list[secoundindex];
			list[secoundindex] = temp;
		}
	}
	#endregion

}
