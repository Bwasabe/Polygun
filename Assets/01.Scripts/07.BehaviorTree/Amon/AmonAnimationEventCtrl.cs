using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmonAnimationEventCtrl : MonoBehaviour
{
    private Amon _amon;

    private void Awake() {
        _amon = transform.parent.GetComponent<Amon>();
    }

    protected void EventProjectileAttack()
    {
        _amon.EventProjectileAttack();
    }
}
