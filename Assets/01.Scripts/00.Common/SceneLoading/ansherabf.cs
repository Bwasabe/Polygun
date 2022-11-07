using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ansherabf : MonoBehaviour
{
    private const string FILE_PATH = "/05.Scenes";

    [MenuItem("asdf/asdf %&A")]
    private static void A()
    {
        string tempFilePath = Path.Combine(Application.dataPath + FILE_PATH);
        string[] s = Directory.GetFileSystemEntries(tempFilePath);
        // string[] s2 = Directory.GetFileSystemEntries(tempFilePath);
        for (int i = 0; i < s.Length; ++i)
        {
            int secondIndex = s[i].LastIndexOf('.');

            Debug.Log($"{s[i]} ◆ {Path.GetFileNameWithoutExtension(s[i])} ◇ {s[i].Substring(secondIndex, s[i].Length - secondIndex)}");
        }
    }
}
