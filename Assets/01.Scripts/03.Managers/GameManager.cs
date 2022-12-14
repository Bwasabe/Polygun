using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoSingleton<GameManager>
{
	public Player Player => _player ??= FindObjectOfType<Player>();

	[SerializeField]
	private Player _player;

	public InputManager InputManager => _inputManager ??= GetComponent<InputManager>() ?? gameObject.AddComponent<InputManager>();

	[SerializeField]
	private InputManager _inputManager;

	[SerializeField]
	private Volume _globalVolume;

	public Volume GlobalVolume => _globalVolume ??= GameObject.Find("Global Volume").GetComponent<Volume>() ?? new GameObject("Global Volume").AddComponent<Volume>();

	[SerializeField]
	private CoinUI coinUI;

	public static float TimeScale = 1f;

	public static float PlayerTimeScale = 1f;

	private int Coin = 0;

	public int CoinAmount
	{
		get
		{
			return Coin;
		}
		set
		{
			Coin = value;
			if (coinUI != null)
				coinUI.CoinSet();
		}
	}
}
