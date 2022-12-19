using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UISceneManager : MonoBehaviour
{
    [SerializeField]
    private Camera _uiCam;

    private void Awake()
    {
        var cameraData = Define.MainCam.GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Add(_uiCam);
    }
}
