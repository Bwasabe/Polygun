using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionCtrl : MonoBehaviour
{
    public event System.Action<Collider2D> ColliderEvent;

    private void OnTriggerEnter2D(Collider2D other) {
        ColliderEvent?.Invoke(other);
    }
}
