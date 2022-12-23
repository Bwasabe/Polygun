using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnParticleObject : MonoBehaviour
{
	[SerializeField]
	private PoolObjectType _poolObjectType;
	private ParticleSystem _particleSystem;
	private void Start()
	{
		_particleSystem = GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		if (!_particleSystem.IsAlive())
			ObjectPool.Instance.ReturnObject(_poolObjectType, this.gameObject);
	}
}
