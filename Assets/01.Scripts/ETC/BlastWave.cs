using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UIElements;
public class BlastWave : MonoBehaviour
{
	public int pointsCount;
	public float maxRadius;
	public float speed;
	public float startWidth;

	private LineRenderer _lineRenderer;
	[SerializeField]
	private GameObject obj;

	private bool _isRun;
	public bool IsRun => _isRun;
	private void Awake()
	{
		_lineRenderer = GetComponent<LineRenderer>();
		_lineRenderer.positionCount = pointsCount + 1;
	}

	//private void Update()
	//{
	//	if(Input.GetKeyDown(KeyCode.LeftShift))
	//	{
	//		StartExplosion();
	//	}
	//}
	private IEnumerator Blast()
	{
		_isRun = true;
		float currentRadius = 0f;

		while(currentRadius < maxRadius)
		{
			currentRadius += Time.deltaTime * speed;
			Draw(currentRadius);
			yield return null;
		}
		_isRun = false;
		//ObjectPool.Instance.ReturnObject(PoolObjectType.ShockWave, this.gameObject);
	}

	private void Draw(float currentRadius)
	{
		float angleBetweenPosints = 360f / pointsCount;

		obj.transform.localScale = new Vector3(currentRadius*2.05f, obj.transform.localScale.y, currentRadius*2.05f);
		for (int i = 0; i<=pointsCount; i++)
		{
			float angle = i * angleBetweenPosints * Mathf.Deg2Rad;
			Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);
			Vector3 position = direction * currentRadius;

			_lineRenderer.SetPosition(i, position);
		}

		//_lineRenderer.widthMultiplier = Mathf.Lerp(0f, startWidth, 1f - currentRadius / maxRadius);
	}
	public void StartExplosion()
	{
		StartCoroutine(Blast());
	}
}
