using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFall : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> _particleSystem = new List<ParticleSystem>();

    private Transform _child;

    private void Awake() {
        _child = transform.GetChild(0);
    }

    public void SetScale(float zScale)
    {
        _particleSystem.ForEach(x =>
        {
            var shape = x.shape;
            if(shape.enabled)
            {
                Vector3 scale = shape.scale;
                scale.z = zScale;
                shape.scale = scale;
            }
        });
        Vector3 pos = _child.transform.localPosition;
        pos.z = zScale * 0.5f;
        _child.transform.localPosition = pos;
    }
    
}
