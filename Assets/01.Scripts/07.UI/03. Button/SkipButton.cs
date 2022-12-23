using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipButton : Btn
{

	[SerializeField]
	private PlayableDirector _director;

	public override void Interaction()
	{
		_director.time = 28.7833f;
	}
}
