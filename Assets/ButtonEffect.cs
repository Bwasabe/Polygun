using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
[RequireComponent(typeof(EventTrigger))]
public class ButtonEffect : MonoBehaviour
{

	public void OnEnter()
	{
		this.transform.DOScale(2f, 0.4f);
	}

	public void OnExit()
	{
		this.transform.DOScale(1f, 0.4f);
	}
}
