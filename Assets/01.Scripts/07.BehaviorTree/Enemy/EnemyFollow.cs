using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
	//이거 나중에 게임매니져에서 가져오기
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
		Vector3 moveNormal = GameManager.Instance.Player.transform.localPosition - this.transform.localPosition;
		ch.Move(moveNormal.normalized * Time.deltaTime * speed);
		//this.transform.position = Vector3.Slerp
		//	(this.transform.position, GameManager.Instance.Player.transform.position, Time.deltaTime*speed);
	}
}
