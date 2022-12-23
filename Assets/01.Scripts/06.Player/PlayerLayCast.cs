using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayCast : BasePlayerComponent
{
	[SerializeField]
	private float _radius;
	[SerializeField]
	private LayerMask _equipmentLayer;

	[SerializeField]
	private GameObject uiObject;
	void Update()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _equipmentLayer);
		if (colliders.Length > 0)
		{
			Chest chest = colliders[0].transform.GetComponent<Chest>();
			if (!chest.isOpen)
				uiObject.SetActive(true);
			if (Input.GetKeyDown(KeyCode.E))
			// ¿Â∫Ò ±∏∏≈ or »πµÊ UI∂ÁæÓ¡÷±‚
			{
				chest.OpenBox();
			}
		}
		else
		{
			uiObject.SetActive(false);
		}
	}
}
