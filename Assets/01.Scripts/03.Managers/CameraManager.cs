using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
	private CinemachineVirtualCamera _virtualCamera;
	public CinemachineImpulseSource impulseSource;
	public CinemachineVirtualCamera mainVCamera
	{
		get
		{
			if (_virtualCamera == null)
				_virtualCamera = GetComponent<CinemachineVirtualCamera>();

			return _virtualCamera;
		}
	}

	public void Awake()
	{
		_virtualCamera = GetComponent<CinemachineVirtualCamera>();
	}


	public void CameraShake()
	{
		impulseSource.GenerateImpulse();
		Debug.LogWarning("?");
	}
}
