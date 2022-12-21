using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _controlManual;

    private void Start() {
        _controlManual.alpha = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ShowControlManual(true);
        }
        else if(Input.GetKeyUp(KeyCode.Tab))
        {
            ShowControlManual(false);
        }
    }

    private void ShowControlManual(bool isShow)
    {
        if (isShow)
            _controlManual.alpha = 1f;
        else
            _controlManual.alpha = 0f;
    }
}
