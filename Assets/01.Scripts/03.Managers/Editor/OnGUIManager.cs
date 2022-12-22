using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGUIManager : MonoSingleton<OnGUIManager>
{
    public Dictionary<string, string> _guiDict = new Dictionary<string, string>();

    [SerializeField]
    private int _fontSize = 30;


    private void OnGUI() {
        #if UNITY_EDITOR
        if(_guiDict.Count <=0)return;
        var label = new GUIStyle();

        label.normal.textColor = Color.red;

        label.fontSize = _fontSize;

        foreach(var dict in _guiDict)
        {
            GUILayout.Label(dict.Value, label);
        }
        #endif
    }
}
