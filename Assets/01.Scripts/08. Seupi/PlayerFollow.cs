using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerFollow : MonoBehaviour
{
 //�̰� ���߿� ���ӸŴ������� ��������
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
