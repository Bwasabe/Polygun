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
	[SerializeField]
	private AudioClip _Selectclip;
	[SerializeField]
	private AudioClip _Onclip;
	public void OnEnter()
	{
		this.transform.DOScale(scale, 0.4f).SetUpdate(true);
	}

	public void OnExit()
	{
		this.transform.DOScale(1f, 0.4f).SetUpdate(true);
	}

	public void ClickSoundStart()
	{
		SoundManager.Instance.Play(AudioType.SFX, _Selectclip);
	}

	public void OnSoundStart()
	{
		SoundManager.Instance.Play(AudioType.SFX, _Onclip);
	}

	public void OnDestroy()
	{
		DOTween.KillAll();
	}

	public void OnReset()
	{
		this.transform.localScale = Vector3.zero;
	}
}
