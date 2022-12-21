using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static bool _shuttingDown = false;
    private static object _locker = new object();
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_shuttingDown)
            {
                Debug.LogWarning("[Instance] Instance" + typeof(T) + "is already destroyed. Returning null.");
                return null;
            }
            lock (_locker)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        _instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                        DontDestroyOnLoad(_instance);
                    }
                }
                return _instance;
            }
        }
    }
    private void Start() {
        _shuttingDown = false;

    }
    // private void OnDestroy()
    // {
    //     Debug.Log("지워짐");
    //     _shuttingDown = true;
    // }
    private void OnApplicationQuit()
    {
        Debug.Log("앱 꺼짐");
        _shuttingDown = true;
    }
}

// using System.Diagnostics.CodeAnalysis;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
// public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
// {
//     public static bool IsInitialized => _instance != null;
//     protected static T _instance;
//     private static readonly object Lock = new();
//     private static bool _applicationIsQuitting;
//     private static bool _isDontDestroyOnLoad;

//     public static T Instance
//     {
//         get
//         {
//             if (_applicationIsQuitting)
//             {
//                 Debug.LogWarning(
//                     $"[MonoSingleton] Instance '{typeof(T)}' already destroyed on application quit. Won't create again - returning null.");
//                 return null;
//             }

//             lock (Lock)
//             {
//                 if (!ReferenceEquals(_instance, null)) return _instance;

//                 _instance = (T)FindObjectOfType(typeof(T));

//                 if (FindObjectsOfType(typeof(T)).Length > 1)
//                 {
//                     Debug.LogError($"[Singleton] {typeof(T)} More than one instance of singleton found!");
//                     return _instance;
//                 }

//                 if (_instance == null)
//                 {
//                     var singleton = new GameObject();
//                     _instance = singleton.AddComponent<T>();
//                     singleton.name = "(singleton) " + typeof(T);

//                     if (_isDontDestroyOnLoad) DontDestroyOnLoad(singleton);

//                     Debug.Log(
//                         $"[Singleton] An instance of {typeof(T)} is needed in the scene, so '{singleton}' was created.");
//                 }
//                 else
//                 {
//                     Debug.Log($"[Singleton] Using instance already created: {_instance.gameObject.name}");
//                 }

//                 return _instance;
//             }
//         }
//     }

//     protected virtual void Awake()
//     {
//         if (_instance == null)
//         {
//             _instance = this as T;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     protected virtual void OnEnable()
//     {
//         _isDontDestroyOnLoad =
//             typeof(T).GetCustomAttributes(typeof(DontDestroyOnLoadAttribute), true).Length > 0;

//         if (_isDontDestroyOnLoad)
//             DontDestroyOnLoad(this);
//         else
//             SceneManager.sceneUnloaded += OnSceneUnloaded;
//     }

//     protected virtual void OnDestroy()
//     {
//         if(_isDontDestroyOnLoad)
//             _applicationIsQuitting = true;
//     }

//     private void OnSceneUnloaded(Scene scene)
//     {
//         if (!_isDontDestroyOnLoad) _instance = null;
//     }
//}
