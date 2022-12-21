using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class BossMap : MapSetting
{
	[SerializeField]
	private GameObject bossObject;
	[SerializeField]
	private PlayableDirector _playableDirect;

	[SerializeField]
	private TimelineAsset[] _clip;

	private DirectBossFirst _direct;

	[SerializeField]
	private AudioClip _bgm;
	[SerializeField]
	private AudioClip _endbgm;
	//private GameObject bossObj;
	public GameObject Boss => bossObject;
	protected override void OnStart()
	{
		bossObject.SetActive(false);
	}
	protected override void OnEnter()
	{
		base.OnEnter();
		bossObject.SetActive(true);
		_playableDirect.playableAsset = _clip[0];
		_playableDirect.Play();
		SoundManager.Instance.Play(AudioType.BGM, _bgm);
	}

	protected override void OnPlay()
	{
		if (!bossObject.activeSelf)
			MapState = MapState.End;
	}

	protected override void OnExit()
	{
		_playableDirect.playableAsset = _clip[1];
		_playableDirect.Play();
	}
	public void BGMStart()
	{
		SoundManager.Instance.Play(AudioType.BGM, _endbgm);
	}
	public void ReturnLobby()
	{
		SceneManager.LoadScene("Lobby");
	}

	public void BossStart()
	{
		GameObject obj = GameObject.Find("Player");
		obj.SetActive(false);
		GameObject canvas = GameObject.Find("PlayerCanvas");
		canvas.SetActive(false);
		SoundManager.Instance.StopBGM();
	}
}
