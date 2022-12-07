    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Player Player => _player ??= FindObjectOfType<Player>();

    private Player _player;

    public InputManager InputManager => _inputManager ??= GetComponent<InputManager>() ?? gameObject.AddComponent<InputManager>();

    private InputManager _inputManager;

    public static float TimeScale = 1f;

    public static float PlayerTimeScale = 1f;
}
