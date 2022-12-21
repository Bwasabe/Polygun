using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private CinemachineVirtualCamera _virtualCamera;
    public CinemachineImpulseSource ImpulseSource
    {
        get
        {
            if (_impulseSource == null)
            {
                _impulseSource = Utils.VCam.GetComponent<CinemachineImpulseSource>();
            }
            return _impulseSource;
        }
    }

    private CinemachineImpulseSource _impulseSource;

    private void Awake()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
	}
    public void CameraShake()
    {
        _impulseSource.GenerateImpulse();
    }
}
