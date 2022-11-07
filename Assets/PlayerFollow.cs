using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
	[SerializeField]
	private GameObject Player; //이거 나중에 게임매니져에서 가져오기

	[SerializeField]
	private float speed;
	private void Update()
	{
		Follow();
	}

	private void Follow()
	{
		this.transform.position = Vector3.Slerp
			(this.transform.position, Player.transform.position, Time.deltaTime*speed);
	}
}
