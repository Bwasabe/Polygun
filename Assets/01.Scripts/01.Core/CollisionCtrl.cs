using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollisionCtrl : MonoBehaviour
{
    public event System.Action<Collider> ColliderEnterEvent;

    public event System.Action<Collider> ColliderExitEvent;

    private void OnTriggerEnter(Collider other) {
        ColliderEnterEvent?.Invoke(other);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    ColliderEnterEvent?.Invoke(collision.collider);
    //}

    private void OnTriggerExit(Collider other) {
        ColliderExitEvent?.Invoke(other);
    }
}
