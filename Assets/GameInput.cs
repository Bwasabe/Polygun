  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameInput : MonoBehaviour
{
	[SerializeField]
	private GameObject settingCanvas = null;
	private bool isSettingOpen = false;
	private void Start()
	{
		settingCanvas.SetActive(false);
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			ToggleSetting();
		}
	}

	public void ToggleSetting()
	{
		isSettingOpen = !isSettingOpen;
		int timeScale = isSettingOpen ? 0 : 1;
		Time.timeScale = timeScale;
		//DOTween.timeScale = timeScale;
		settingCanvas.SetActive(isSettingOpen);
	}
}
