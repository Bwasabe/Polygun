using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Yields;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private TMPro.TMP_Text _text;
    [SerializeField]
    private float _minLoadTime = 1f;

    private float _timer = 0f;
    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(Define.LoadingSceneName);
        operation.allowSceneActivation = false;
        while (operation.progress < 0.9f)
        {
            _slider.value = operation.progress;
            _text.text = string.Format("{0}%", (int)(operation.progress * 100f));
            yield return null;
            Debug.Log(operation.progress);
        }
        Debug.Log("끝");
        // while (_timer <= 0.99f)
        // {
        //     _timer += Time.deltaTime;
        //     if(_timer >= 1f)
        //     {
        //         _timer = 0.99f;
        //     }
        //     _slider.value = _timer;
        //     _text.text = string.Format("{0}%", (int)(_slider.value * 100f));
        //     yield return null;
        // }
        _text.text = "100%";
        yield return WaitForSeconds(0.5f);
        if (Define.LoadingSceneName.Equals("Release"))
        {
            Debug.Log("로딩씬 ");
            SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
        }
        operation.allowSceneActivation = true;
    }
}
