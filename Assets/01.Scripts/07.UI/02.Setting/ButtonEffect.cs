using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
[RequireComponent(typeof(EventTrigger))]
public class ButtonEffect : MonoBehaviour
{
	[SerializeField]
	private float scale;

	public void OnEnter()
	{
		this.transform.DOScale(scale, 0.4f);
	}

	public void OnExit()
	{
		this.transform.DOScale(1f, 0.4f);
	}

	public void OnDestroy()
	{
		DOTween.KillAll();
	}
}
