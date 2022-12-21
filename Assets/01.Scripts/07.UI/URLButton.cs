using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLButton : MonoBehaviour
{
    [SerializeField]
    private string _url;

    public void OnClickURL()
    {
        Application.OpenURL(_url);
    }
}
