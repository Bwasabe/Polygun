using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveButton : MonoBehaviour
{
    [SerializeField]
    private GameObject _falseObejct;
    [SerializeField]
    private GameObject _trueObject;

    public void ChangeObject()
    {
        _falseObejct.SetActive(false);
        _trueObject.SetActive(true);
    }
}
