using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public Dictionary<string, KeyCode> InputDict { get; private set; } = new Dictionary<string, KeyCode>();

    /// <summary>
    /// 키를 더해줄 때 사용하는 함수
    /// </summary>
    /// <param name="key"> Dictionary에 들어갈 Key값 </param>
    /// <param name="value"> Key에 맞는 Keycode값 </param>
    public void AddInput(string key, KeyCode value)
    {
        KeyCode keyCode;
        if (InputDict.TryGetValue(key, out keyCode))
        {
            Debug.LogError($"{key} is already exist by {keyCode}");
            return;
        }
        else
        {
            InputDict.Add(key, value);
        }
    }

    /// <summary>
    /// Input을 지울 때 사용하는 함수
    /// </summary>
    /// <param name="keyName"> 지울 키의 이름 </param>
    public void RemoveInput(string keyName)
    {
        if (InputDict.ContainsKey(keyName))
        {
            InputDict.Remove(keyName);
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
        if (InputDict.TryGetValue(newKey, out keyCode))
        {
            InputDict[newKey] = InputDict[oldKey];
            InputDict[oldKey] = keyCode;
        }
        else
        {
            AddInput(newKey, InputDict[oldKey]);
            RemoveInput(oldKey);
        }
    }

}
