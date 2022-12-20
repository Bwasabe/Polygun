using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Btn : MonoBehaviour
{
	[SerializeField]
	private string loadSceneText;

	public virtual void Interaction()
	{
        Define.LoadingSceneName = loadSceneText;
        SceneManager.LoadScene("LoadingScene");
	}
}
