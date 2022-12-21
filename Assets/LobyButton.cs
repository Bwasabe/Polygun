using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class LobyButton : Btn
{
    [SerializeField]
    private GameObject _canvas;
    [SerializeField]
    private PlayableDirector _palyDirect;
    public override void Interaction()
    {
        _canvas.SetActive(false);
		SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
		_palyDirect.Play();
	}
}
