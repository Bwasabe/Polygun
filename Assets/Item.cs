using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionCtrl))]
[DisallowMultipleComponent]
public class Item : MonoBehaviour, IPurchaseAble
{
	private CollisionCtrl _collision;

	[SerializeField]
	private LayerMask _layerMask;

	[SerializeField]
	private int _price;

    public int Price => _price;

    private void Awake()
	{
		_collision = GetComponent<CollisionCtrl>();
		_collision.CollisionEnterEvent += Interaction;
	}

	protected virtual void Interaction(Collision other)
	{

	}

	protected virtual bool IsInteraction(Collision other)
	{
		return ((1 << other.gameObject.layer) & _layerMask) > 0;
	}

    public void PurchaseCallBack()
    {
        
    }
}
