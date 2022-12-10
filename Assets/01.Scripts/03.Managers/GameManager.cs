    using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoSingleton<GameManager>
{
    public Player Player => _player ??= FindObjectOfType<Player>();

    private Player _player;

    public InputManager InputManager => _inputManager ??= GetComponent<InputManager>() ?? gameObject.AddComponent<InputManager>();

    private InputManager _inputManager;

    public static float TimeScale = 1f;

    public static float PlayerTimeScale = 1f;

    public int CoinAmount = 0;

    private void Start()
    {
		OnGUIManager.Instance._guiDict.Add("asdf", $"{CoinAmount}");
	}
    private void Update()
    {
		OnGUIManager.Instance._guiDict["asdf"] = $"{CoinAmount}";
	}
}
