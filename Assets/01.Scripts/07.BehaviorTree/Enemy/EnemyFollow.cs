using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
	//이거 나중에 게임매니져에서 가져오기
	[SerializeField]
	private float speed;
	[SerializeField]
	private LayerMask _groundLayer;
	[SerializeField]
	private float _gravityScale;

	CharacterController ch;
	Vector3 vec = Vector3.zero;
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
