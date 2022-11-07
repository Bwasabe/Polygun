using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private Dictionary<string, KeyCode> _inputDict = new Dictionary<string, KeyCode>();

    /// <summary>
    /// 키를 더해줄 때 사용하는 함수
    /// </summary>
    /// <param name="key"> Dictionary에 들어갈 Key값 </param>
    /// <param name="value"> Key에 맞는 Keycode값 </param>
    public void AddInput(string key, KeyCode value)
    {
        KeyCode keyCode;
        if (_inputDict.TryGetValue(key, out keyCode))
        {
            Debug.LogError($"{key} is already exist by {keyCode}");
            return;
        }
        else
        {
            _inputDict.Add(key, value);
        }
    }

    public KeyCode GetInput(string key)
    {
        if(_inputDict.ContainsKey(key))
        {
            return _inputDict[key];
        }
        else
        {
            throw new System.Exception($"{key} is None");
        }
    }

    /// <summary>
    /// Input을 지울 때 사용하는 함수
    /// </summary>
    /// <param name="keyName"> 지울 키의 이름 </param>
    public void RemoveInput(string keyName)
    {
        if (_inputDict.ContainsKey(keyName))
        {
            _inputDict.Remove(keyName);
        }
        else
        {
            Debug.LogError($"{keyName} is None");
        }
    }

    /// <summary>
    /// 키를 변경할 때 사용하는 함수
    /// </summary>
    /// <param name="oldKey"> 변경하기 전 Key값 </param>
    /// <param name="newKey"> 변경한 후 Key값 </param>
    public void ChangeInput(string oldKey, string newKey)
    {
        KeyCode keyCode;
        if (_inputDict.TryGetValue(newKey, out keyCode))
        {
            _inputDict[newKey] = _inputDict[oldKey];
            _inputDict[oldKey] = keyCode;
        }
        else
        {
            AddInput(newKey, _inputDict[oldKey]);
            RemoveInput(oldKey);
        }
    }

}
