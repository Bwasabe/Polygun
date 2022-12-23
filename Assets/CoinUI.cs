using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
	[SerializeField]
	TMP_Text m_textMeshPro;

	private void Start()
	{
		m_textMeshPro = GetComponent<TMP_Text>();
	}
	public void CoinSet()
	{
		Debug.Log(m_textMeshPro);
		Debug.Log(GameManager.Instance.CoinAmount);
		m_textMeshPro.text = GameManager.Instance.CoinAmount.ToString();
	}
}
