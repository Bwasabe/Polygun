using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seupi : MonoBehaviour
{
	private float lerpTime = 0;

	[Header("X俊 包茄 加己")]
	public float xSpeed = 2f;
	public float xPos = 1f;
	public float xPositon;

	[Header("Y俊 包茄 加己")]
	public float ySpeed = 2f;
	public float yPos = 1f;
	public float yPositon;

	private void Update()
	{
		lerpTime += Time.deltaTime;
		Move();
	}

	private void Move()
	{
		this.transform.localPosition = new Vector3(xPos*Mathf.Sin(lerpTime * xSpeed) + xPositon,
			yPos * Mathf.Sin(lerpTime * ySpeed) + yPositon, this.transform.localPosition.z);
	}
}
