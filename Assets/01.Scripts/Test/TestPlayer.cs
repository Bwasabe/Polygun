using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public float speed;

	private Rigidbody rigid;
	private void Start()
	{
		rigid = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
    {
		rigid.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime;
    }
}
