using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutButton : Btn
{
	[SerializeField]
	private GameObject _obj;
	public override void Interaction()
	{
		_obj.SetActive(false);
	}
}
