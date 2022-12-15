using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
	//�̰� ���߿� ���ӸŴ������� ��������
	[SerializeField]
	private float speed;

	CharacterController ch;
	private void Awake()
	{
		ch = GetComponent<CharacterController>();
	}
	private void Update()
	{
		Follow();
	}

	private void Follow()
	{
		this.transform.LookAt(GameManager.Instance.Player.transform);
	}
}
