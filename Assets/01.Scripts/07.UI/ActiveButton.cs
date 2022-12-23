using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveButton : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _falseObejct;
    [SerializeField]
    private List<GameObject> _trueObject;

    public void ChangeObject()
    {
        _falseObejct.ForEach(x => x?.SetActive(false));
        _trueObject.ForEach(x => x?.SetActive(true));
    }
}
