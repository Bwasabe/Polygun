using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : Btn
{
	public override void Interaction()
	{
		Application.Quit();
	}
}
