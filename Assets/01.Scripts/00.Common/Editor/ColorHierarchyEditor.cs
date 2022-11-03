using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ColorHierarchyEditor
{
    [MenuItem("ColorHierarchy/AddColorHierarchy")]
    [MenuItem("GameObject/ColorHierarchy/AddColorHierarchy %H")]
    private static void CreateColorHierarchy()
    {
        GameObject[] obj = Selection.gameObjects;


        for (int i = 0; i < obj.Length; i++)
        {
            if(obj[i].GetComponent<ColorHierarchy>() == null)
                Undo.AddComponent<ColorHierarchy>(obj[i]);
        }
    }

    [MenuItem("ColorHierarchy/RemoveColorHierarchy")]
    [MenuItem("GameObject/ColorHierarchy/RemoveColorHierarchy %#H")]
    private static void RemoveColorHierarchy()
    {
        GameObject[] obj = Selection.gameObjects;

        for (int i = 0; i < obj.Length; i++)
        {
            ColorHierarchy ch = obj[i].GetComponent<ColorHierarchy>();
                Undo.DestroyObjectImmediate(ch);
        }
    }
}
