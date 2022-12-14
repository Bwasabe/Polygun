using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public static class Utils
{
    public static CinemachineFreeLook VCam
    {
        get
        {
            if (_vCam == null)
            {
                _vCam = GameObject.FindObjectOfType<CinemachineFreeLook>();
            }
            return _vCam;
        }
    }

    private static CinemachineFreeLook _vCam;
}
