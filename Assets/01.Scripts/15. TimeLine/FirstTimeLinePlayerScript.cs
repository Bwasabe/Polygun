using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class FirstTimeLinePlayerScript : MonoBehaviour
{
	[SerializeField]
	private GameObject obj;

	[SerializeField]
	private Animator ani;

	[SerializeField]
	private GameObject canvasObj;
	[SerializeField]
	private PlayableDirector _playableDirector;

	[SerializeField]
	private float _fadeTime;
	public void PlayerActive()
	{
		this.transform.parent = obj.transform;
		this.transform.localPosition = new Vector3(0.3f,3.5f,4.2f);
	}

	public void Attack()
	{
		ani.SetTrigger("attack_02");
	}
	public void Fade()
	{
		Define.FadeParent.Fade(1, _fadeTime);
	}
	public void SetLobbyScene()
	{
		canvasObj.SetActive(true);
		_playableDirector.Pause();
	}

	public void End()
	{
		Define.LoadingSceneName = BuildingScenes.Release.ToString();
		SceneManager.LoadScene(BuildingScenes.LoadingScene.ToString());
	}

	public void SkipTimeline()
	{
		_playableDirector.time = 28.9833f;
	}
}
