using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PracticeButton : Btn
{
	public override void Interaction()
	{
		Define.LoadingSceneName = BuildingScenes.Practice.ToString();
		base.Interaction();
	}
}
