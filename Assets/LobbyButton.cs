using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyButton : Btn
{
	public override void Interaction()
	{
		Define.LoadingSceneName = BuildingScenes.Action.ToString();
		base.Interaction();
	}
}
