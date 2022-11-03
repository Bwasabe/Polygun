using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Player Player
    {
        get
        {
            return _player ??= FindObjectOfType<Player>();
        }

    }

    private Player _player;
}
