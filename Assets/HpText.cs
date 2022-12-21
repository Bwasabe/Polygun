using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HpText : MonoBehaviour
{
	[SerializeField]
	private Player player;

	private TMP_Text _text;

	private float maxHp;
	private void Start()
	{
		_text = GetComponent<TMP_Text>();
		maxHp = player.PlayerStat.HP;
		SetHp();
	}

	public void SetHp()
	{
		_text.text = $"{player.PlayerStat.HP}/{maxHp}";
	}
}
