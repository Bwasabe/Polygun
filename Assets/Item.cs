using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionCtrl))]
[DisallowMultipleComponent]
public class Item : MonoBehaviour
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
		_collision.ColliderEnterEvent += Interaction;
		_collision.CollisionStayEvent += Interaction;
		//_collision.CollisionEnterEvent += Interaction;
	}

	protected virtual void Interaction(Collider other)
	{

	}

	protected virtual void Interaction(Collision other)
	{

	}

	protected virtual bool IsInteraction(Collider other)
	{
		return ((1 << other.gameObject.layer) & _layerMask) > 0;
	}

	protected virtual bool IsInteraction(Collision other)
	{
		return ((1 << other.gameObject.layer) & _layerMask) > 0;
	}

}
