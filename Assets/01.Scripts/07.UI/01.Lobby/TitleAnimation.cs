using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TitleAnimation : MonoBehaviour
{
	private TMP_Text _title_Text;
	[Header("효과 사용할 것들")]
	public bool isWave;

	[Header("웨이브 변수들")]
	public float speed;
	public float power;
	public float numPower;


	private void Awake()
	{
		_title_Text = GetComponent<TMP_Text>();
	}

	private void Update() {
		if (isWave)
			Wave();
	}

	private void Wave()
	{
		_title_Text.ForceMeshUpdate();
		var textInfo = _title_Text.textInfo;

		for (int i = 0; i < textInfo.characterCount; ++i)
		{
			var charInfo = textInfo.characterInfo[i];
			if (!charInfo.isVisible)
			{
				continue;
			}

			var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

			for (int j = 0; j < 4; ++j)
			{
				var orig = verts[charInfo.vertexIndex + j];
				verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * speed + orig.x * numPower) * power, 0);
			}
		}
		for (int i = 0; i < textInfo.meshInfo.Length; ++i)
		{
			var meshInfo = textInfo.meshInfo[i];
			meshInfo.mesh.vertices = meshInfo.vertices;
			_title_Text.UpdateGeometry(meshInfo.mesh, i);
		}
	}
}
