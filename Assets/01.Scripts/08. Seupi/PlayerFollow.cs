using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
	[SerializeField]
	private GameObject Player; //�̰� ���߿� ���ӸŴ������� ��������

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
