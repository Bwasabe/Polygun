using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public enum MiniMapType
{
	StartMap,
	NormalMap,
	StoreMap,
	EffectMap,
	BossMap,
}
public class MiniMap : MonoSingleton<MiniMap>
{
	[SerializeField]
	private List<GameObject> minimaps;
	[SerializeField]
	private GameObject parent;

	private RectTransform parentRect;

	private List<GameObject> objects = new List<GameObject>();

	private RectTransform[,] rect;

	[SerializeField]
	RectTransform playerimage;
	private void Awake()
	{
		parentRect = parent.GetComponent<RectTransform>();
	}

	private void Update()
	{
		Vector3 vec = Define.MainCam.transform.localEulerAngles;
		//Vector3 vec2 = GameManager.Instance.Player.transform.localEulerAngles;
		//if (vec.y > 0 && vec.y < 90)
		//	vec.y = -vec.y;
		//parent.transform.localRotation = Quaternion.Euler(0, 0, vec2.y - 90);
		playerimage.transform.localRotation = Quaternion.Euler(0, 0,-vec.y + 90f);
	}

	//public void AllMiniMapSetting(Map[,] maps)
	//{
	//	float size = Mathf.Sqrt(maps.Length);
	//	float height = parentRect.rect.height / size;
	//	float width = parentRect.rect.width / size;

	//	rect = new RectTransform[(int)size, (int)size];
	//	for (int i = 0; i<size; i++)
	//	{
	//		for(int j = 0; j<size; j++)
	//		{
	//			if (maps[i,j] != null)
	//			{
	//				GameObject mapObj = MiniMapObjSelect(maps[i, j].roomType);
	//				GameObject obj = Instantiate(mapObj, parent.transform);

	//				RectTransform mapRect = obj.GetComponent<RectTransform>();
	//				mapRect.sizeDelta = new Vector3(width,height);
	//				mapRect.anchoredPosition = new Vector2(i * height, j * width);
	//				rect[i, j] = mapRect;
	//			}
	//		}
	//	}
	//}

	//public void AMiniMapSet(Vector2Int pos)
	//{
	//	playerimage.parent = rect[pos.x, pos.y];
	//	playerimage.localPosition = Vector2.zero;
	//}
	public void MiniMapSet(Map thisMap)
	{
		MiniMapClear();
		foreach (Map map in thisMap.moveMaps)
		{
			GameObject mapObj = MiniMapObjSelect(map.roomType);
			GameObject obj = null;
			Vector2 vec = Vector2.zero;
			Debug.Log(map.pos);
			if (map.pos.x + 1 == thisMap.pos.x && map.pos.y == thisMap.pos.y)
			{
				Debug.Log("왼쪽");
				vec = Vector2.down * 100;
			}
			else if (map.pos.x - 1 == thisMap.pos.x && map.pos.y == thisMap.pos.y)
			{
				Debug.Log("오른쪽");
				vec = Vector2.up * 100;
			}
			else if (map.pos.x == thisMap.pos.x && map.pos.y + 1 == thisMap.pos.y)
			{
				Debug.Log("위");
				vec = Vector2.right * 100;
			}
			else if (map.pos.x == thisMap.pos.x && map.pos.y - 1 == thisMap.pos.y)
			{
				Debug.Log("아래");
				vec = Vector2.left * 100;
			}

			obj = Instantiate(mapObj, parent.transform);
			obj.GetComponent<RectTransform>().anchoredPosition = vec;
			objects.Add(obj);
		}
	}

	public void MiniMapClear()
	{
		if (objects.Count <= 0)
			return;

		foreach (GameObject obj in objects)
		{
			Destroy(obj);
		}

		objects.Clear();
	}

	private GameObject MiniMapObjSelect(RoomType roomType)
	{
		if (roomType == RoomType.StoreRoom)
		{
			return minimaps[(int)MiniMapType.StoreMap];
		}
		else if (roomType == RoomType.StartRoom)
		{
			return minimaps[(int)MiniMapType.StartMap];
		}
		else if (roomType == RoomType.BossRoom)
		{
			return minimaps[(int)MiniMapType.BossMap];
		}
		else
		{
			return minimaps[(int)MiniMapType.NormalMap];
		}
	}
}
