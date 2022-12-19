using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstTimeLinePlayerScript : MonoBehaviour
{
	[SerializeField]
	private GameObject obj;

	[SerializeField]
	private Animator ani;
	public void PlayerActive()
	{
		this.transform.parent = obj.transform;
		this.transform.localPosition = new Vector3(0.3f,3.5f,4.2f);
	}

	public void Attack()
	{
		ani.SetTrigger("attack_02");
	}

	public void End()
	{
		SceneManager.LoadScene("Release");
	}
}
