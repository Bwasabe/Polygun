using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed;

    public bool IsFreeze { get; set; } = false;

    private CharacterController _cc;
    private void Awake() {
        _cc = GetComponent<CharacterController>();
    }

    private void Update() {
        Move();
    }

    private void Move()
    {
        if(IsFreeze)return;
        
    }
}
