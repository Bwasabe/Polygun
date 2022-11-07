using UnityEngine;

public abstract class BaseCollision : MonoBehaviour
{
    protected CollisionCtrl _collisionCtrl;

    protected virtual void Start() {
        RegisterCollisionEvent();
    }
    protected abstract void RegisterCollisionEvent();
}
