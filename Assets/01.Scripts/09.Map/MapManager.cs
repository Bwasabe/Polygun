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

public class MapManager : MonoBehaviour
{
	public int mapMaxCreateCount;
	private int mapCreateCount = 0;
	private bool[,] mapCreateArray;
	private bool[,] mapisSearchArray;
	private Map[,] mapInfoArray;

	public GameObject debugObj;

	private Queue<Pair<int, int>> pairQueue;

	private List<Map> EndMaps = new List<Map>();
	private void Start()
	{
		mapCreateArray = new bool[mapMaxCreateCount, mapMaxCreateCount];
		mapisSearchArray = new bool[mapMaxCreateCount, mapMaxCreateCount];
		mapInfoArray = new Map[mapMaxCreateCount, mapMaxCreateCount];
		pairQueue = new Queue<Pair<int, int>>();

		mapCreateCount = 0;


		MapCreate(mapMaxCreateCount / 2, mapMaxCreateCount / 2);
		while (pairQueue.Count != 0 && mapCreateCount < mapMaxCreateCount)
		{
			Pair<int, int> pair = pairQueue.Dequeue();
			MapCreate(pair.first, pair.secound);
		}
		MapSearch(mapMaxCreateCount / 2, mapMaxCreateCount / 2, null);
		MapDoorSet();
	}

	private void MapCreate(int x, int y)
	{
		if (mapCreateCount >= mapMaxCreateCount)
			return;

		mapCreateArray[x, y] = true;
		GameObject obj = Instantiate(debugObj, transform);
		obj.transform.position = new Vector3(x * 20, 0, y * 20);
		mapInfoArray[x, y] = obj.GetComponent<Map>();
		mapInfoArray[x, y].pos = new Vector2Int(x, y);
		mapCreateCount++;

		List<Direction> dir = new List<Direction>();
		for (int i = 0; i < 4; i++)
		{
			dir.Add((Direction)i);
		}

		DirRemove(ref dir, x, y);

		int rand = Random.Range(1, dir.Count);
		ShuffleArray(dir);

		for (int i = 0; i < rand; i++)
		{
			Pair<int, int> pair = DirToPair(dir[i]);
			pairQueue.Enqueue(new Pair<int, int>(x + pair.first, y + pair.secound));
		}
	}

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
			int firstindex = Random.Range(0, list.Count - 1);
			int secoundindex = Random.Range(0, list.Count - 1);

			T temp = list[firstindex];
			list[firstindex] = list[secoundindex];
			list[secoundindex] = temp;
		}
	}

	private void MapSearch(int x, int y, Map beforeMap)
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

		if (beforeMap != null)
		{
			if (mapInfoArray[x, y].moveMaps.Count != 0)
			{
				int Randoms = Random.Range(0, 1);
				if (Randoms != 0)
				{
					mapInfoArray[x, y].moveMaps.Add(beforeMap);
					beforeMap.moveMaps.Add(mapInfoArray[x, y]);
				}
			}
			else
			{
				mapInfoArray[x, y].moveMaps.Add(beforeMap);
				beforeMap.moveMaps.Add(mapInfoArray[x, y]);
			}
		}


		Pair<int, int> pair = DirToPair(Direction.Foword);
		MapSearch(x + pair.first, y + pair.secound, mapInfoArray[x, y]);
		pair = DirToPair(Direction.Back);
		MapSearch(x + pair.first, y + pair.secound, mapInfoArray[x, y]);
		pair = DirToPair(Direction.Left);
		MapSearch(x + pair.first, y + pair.secound, mapInfoArray[x, y]);
		pair = DirToPair(Direction.Right);
		MapSearch(x + pair.first, y + pair.secound, mapInfoArray[x, y]);
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

					if(mapInfoArray[i, j].moveMaps.Count == 1 &&
						!(i == mapCreateCount /2 && j == mapCreateCount/2))
					{
						EndMaps.Add(mapInfoArray[i, j]);
					}
				}
			}
		}
	}
}
