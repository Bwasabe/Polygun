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

	private List<GameObject> objects = new List<GameObject>();
	public void MiniMapSet(Map thisMap)
	{
		foreach(Map map in thisMap.moveMaps)
		{
			GameObject mapObj = MiniMapObjSelect(map.roomType);
			GameObject obj = null;
			Vector2 vec = Vector2.zero;
			Debug.Log(map.pos);
			if(map.pos.x + 1 == thisMap.pos.x && map.pos.y == thisMap.pos.y)
			{
				Debug.Log("왼쪽");
				vec = Vector2.down * 100;
			}
			else if(map.pos.x - 1 == thisMap.pos.x && map.pos.y == thisMap.pos.y)
			{
				Debug.Log("오른쪽");
				vec = Vector2.up * 100;
			}
			else if(map.pos.x == thisMap.pos.x && map.pos.y + 1 == thisMap.pos.y)
			{
				Debug.Log("위");
				vec = Vector2.left* 100;
			}
			else if(map.pos.x == thisMap.pos.x && map.pos.y - 1 == thisMap.pos.y)
			{
				Debug.Log("아래");
				vec = Vector2.right * 100;
			}

			obj = Instantiate(mapObj, parent.transform);
			obj.GetComponent<RectTransform>().anchoredPosition = vec;
			objects.Add(obj);
		}
	}

	//public void MiniMapClear()
	//{
	//	if (objects.Count <= 0)
	//		return;

	//	foreach (GameObject obj in objects)
	//	{
	//		Destroy(obj);
	//		objects?.Remove(obj);
	//	}
	//}

	private GameObject MiniMapObjSelect(RoomType roomType)
	{
		if(roomType == RoomType.StoreRoom)
		{
			return minimaps[(int)MiniMapType.StoreMap];
		}
		//else if(roomType == RoomType.EffectRoom)
		//{
		//	return minimaps[(int)MiniMapType.EffectMap];
		//}
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
