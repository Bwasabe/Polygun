using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerFollow : MonoBehaviour
{
 //이거 나중에 게임매니져에서 가져오기
	[SerializeField]
	private float speed;
	private void Update()
	{
		Follow();
	}

	private void Follow()
	{
		this.transform.position = Vector3.Slerp
			(this.transform.position, GameManager.Instance.Player.transform.position, Time.deltaTime * speed);
	}
}
