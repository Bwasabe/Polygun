using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
	[SerializeField]
	Button[] _buttons;

	[SerializeField]
	private GameInput _input;
	[SerializeField]
	private Setting _setting;

	private bool _settingToggle;
	private enum SettingButtons
	{
		Continue,
		Setting,
		Lobby,
		Exit
	}

	private void Awake()
	{
		_buttons[(int)SettingButtons.Continue].onClick.AddListener(() =>
		{
			//ButtonReset();
			_input.ToggleSetting();
			_setting.SettingToggle(false);
		});
		_buttons[(int)SettingButtons.Setting].onClick.AddListener(() =>
		{
			_settingToggle = !_settingToggle;
			_setting.SettingToggle(_settingToggle);
		});
		_buttons[(int)SettingButtons.Lobby].onClick.AddListener(() =>
		{
			_input.ToggleSetting();
			_setting.SettingToggle(false);
			DOTween.KillAll();
			SceneManager.LoadScene("Lobby");
		});
		_buttons[(int)SettingButtons.Exit].onClick.AddListener(() =>
		{
			_input.ToggleSetting();
			_setting.SettingToggle(false);
			DOTween.KillAll();
			Application.Quit();
		});
	}


	public void ButtonReset()
	{
		foreach (Button b in _buttons)
		{
			b.GetComponent<ButtonEffect>().OnReset();
		}
	}
}
