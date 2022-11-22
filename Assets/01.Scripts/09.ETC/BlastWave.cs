using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlastWave : MonoBehaviour
{
	public int pointsCount;
	public float maxRadius;
	public float speed;
	public float startWidth;

	private LineRenderer lineRenderer;
	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.positionCount = pointsCount + 1;
	}


	private IEnumerator Blast()
	{
		float currentRadius = 0f;

		while(currentRadius < maxRadius)
		{
			currentRadius += Time.deltaTime * speed;
			Draw(currentRadius);
			yield return null;
		}
	}

	private void Draw(float currentRadius)
	{
		float angleBetweenPosints = 360f / pointsCount;

		for(int i = 0; i<=pointsCount; i++)
		{
			float angle = i * angleBetweenPosints * Mathf.Deg2Rad;
			Debug.Log(angle);
			Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);
			Debug.Log(direction.x);
			Vector3 position = direction * currentRadius;

			lineRenderer.SetPosition(i, position);
		}

		lineRenderer.widthMultiplier = Mathf.Lerp(0f, startWidth, 1f - currentRadius / maxRadius);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
			StartCoroutine(Blast());
	}
}
