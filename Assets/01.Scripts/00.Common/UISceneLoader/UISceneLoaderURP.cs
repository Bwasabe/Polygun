// using static Define;
// using static Yields;

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.Rendering.Universal;

// public class UISceneLoaderURP : MonoSingleton<UISceneLoaderURP>
// {
//     private static readonly string[] IGNORESCENES = { "UIScene", "StartScene" };
//     private const string UISCENE = "UIScene";
//     private const string UICAM = "UICam";
//     static AsyncOperation _async = new AsyncOperation();
//     private const string UI = "UI";
//     private static Transform _uiObject;

//     [RuntimeInitializeOnLoadMethod]
//     private static void LoadingUIScene()
//     {
//         if (CheckIgnoreScene()) return;
//         StartAfterCor(Instance);
//     }

//     private static bool CheckIgnoreScene()
//     {
//         for (int i = 0; i < IGNORESCENES.Length; ++i)
//         {
//             if (SceneManager.GetActiveScene().name.Equals(IGNORESCENES[i]))
//             {
//                 return true;
//             }
//         }
//         return false;
//     }

//     public static void StartAfterCor(MonoBehaviour script)
//     {
//         script.StartCoroutine(AfterLoadUIScene());
//     }

//     public static IEnumerator AfterLoadUIScene()
//     {
//         _async = SceneManager.LoadSceneAsync(UISCENE, LoadSceneMode.Additive);
//         yield return WaitUntil(() => _async.isDone);
//         GameObject[] objs = SceneManager.GetSceneByName(UISCENE).GetRootGameObjects();
//         for (int i = 0; i < objs.Length; i++)
//         {
//             if (objs[i].name.Equals(UICAM))
//             {
//                 UICam = objs[i].GetComponent<Camera>();
//             }
//             else if(objs[i].name.Equals(UI))
//             {
//                 _uiObject = objs[i].transform;
//             }
//         }
//         for (int i = 0; i < objs.Length; ++i)
//         {
//             objs[i].transform.SetParent(_uiObject);
//         }
//         //SceneManager.MoveGameObjectToScene(_uiObject.gameObject, SceneManager.GetActiveScene());

//         var cameraData = MainCam.GetUniversalAdditionalCameraData();
//         cameraData.cameraStack.Add(UICam);
//         // SceneManager.UnloadSceneAsync(UISCENE);
//     }




// }
