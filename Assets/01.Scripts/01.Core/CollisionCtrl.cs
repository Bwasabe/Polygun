using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollisionCtrl : MonoBehaviour
{
    public event System.Action<Collider> ColliderEnterEvent;
    public event System.Action<Collider> ColliderStayEvent;
    public event System.Action<Collider> ColliderExitEvent;

    public event System.Action<Collision> CollisionEnterEvent;
    public event System.Action<Collision> CollisionStayEvent;
    public event System.Action<Collision> CollisionExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        ColliderEnterEvent?.Invoke(other);
    }
    private void OnTriggerStay(Collider other) {
        ColliderStayEvent?.Invoke(other);
    }
    private void OnTriggerExit(Collider other)
    {
        ColliderExitEvent?.Invoke(other);
    }


    private void OnCollisionEnter(Collision other)
    {
        CollisionEnterEvent?.Invoke(other);
    }
    private void OnCollisionStay(Collision other)
    {
        CollisionStayEvent?.Invoke(other);
    }
    private void OnCollisionExit(Collision other)
    {
        CollisionExitEvent?.Invoke(other);
    }

}
