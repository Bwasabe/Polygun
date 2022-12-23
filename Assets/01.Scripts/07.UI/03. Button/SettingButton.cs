using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingButton : Btn
{
	[SerializeField]
	private GameObject _obj;
	public override void Interaction()
	{
		_obj.SetActive(true);
	}
}
