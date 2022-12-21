using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditButton : Btn
{
	[SerializeField]
	private GameObject _obj;
	public override void Interaction()
	{
		_obj.SetActive(true);
	}
}
