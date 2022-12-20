using System;
using UnityEngine;
using Random = UnityEngine.Random;


public static class Define
{

    public static Camera MainCam
    {
        get
        {
            if (_mainCam == null)
            {
                _mainCam = Camera.main;
            }
            return _mainCam;
        }

    }

    private static Camera _mainCam;

    public static Camera UICam
    {
        get
        {
            return _uiCam;
        }
        set
        {
            if (_uiCam == null)
            {
                _uiCam = value;
            }
            else
            {
                return;
            }
        }
    }

    private static Camera _uiCam;

    public static FadeParent FadeParent => _fadeParent ??= GameObject.FindObjectOfType<FadeParent>();

    private static FadeParent _fadeParent;

    public const int DEFAULT_RANDOM_NUM = -1;

    public static string LoadingSceneName = "";


    public static Vector2 MousePos => MainCam.ScreenToWorldPoint(Input.mousePosition);

    public static T GetRandomEnum<T>(bool isNone = false, bool isLength = false)
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(Random.Range((isNone) ? 1 : 0, (isLength) ? values.Length - 1 : values.Length));
    }

    public static T GetEnumValue<T>(this Enum e) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        return (T)(object)e;
    }
}
