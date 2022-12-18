using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
	[SerializeField]
	private GameObject _settingPanel;

	private bool _isSettingActive;
	private void Awake()
	{
		SettingToggle(false);
	}

	public void SettingToggle(bool isActive)
	{
		_isSettingActive = isActive;
		_settingPanel.SetActive(_isSettingActive);
	}
}
