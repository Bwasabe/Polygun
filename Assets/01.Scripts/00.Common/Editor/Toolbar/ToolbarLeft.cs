using System.Linq;
using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using UnityToolbarExtender;
using System.IO;

[InitializeOnLoad]
public class ToolbarLeft
{
    private const string SCENES_FILE_PATH = "/05.Scenes";
    static ToolbarLeft()
    {
        ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
    }

    private static void OnToolbarGUI(IMGUIEvent evt)
    {
        Debug.Log($"OnToolbarGUI {evt.target}");
    }

    static void OnToolbarGUI()
    {
        GUIContent content = new GUIContent(EditorSceneManager.GetActiveScene().name);

        string filePath = $"{Application.dataPath}{SCENES_FILE_PATH}";//Path.Combine(Application.dataPath, SCENES_FILE_PATH);

        // Debug.Log(Path.Combine(Application.dataPath, SCENES_FILE_PATH));

        Rect dropdownRect = new Rect(5, 0, 150, 20);
        if (EditorGUI.DropdownButton(dropdownRect, content, FocusType.Keyboard, EditorStyles.toolbarDropDown))
        {
            MakeSceneMenus(filePath, dropdownRect);
            // menu.DropDown(dropdownRect);

        }
    }

    private static void MakeSceneMenus(string path, Rect dropdownRect)
    {

        GenericMenu menu = new GenericMenu();

        string[] scenes = Directory.GetFileSystemEntries(path);

        foreach (var scene in scenes)
        {
            int dotIndex = scene.LastIndexOf('.');
            string substring = scene.Substring(dotIndex);
            if (substring != ".meta")
            {
                string extension = Path.GetFileNameWithoutExtension(scene);
                int extenDotIndex = extension.LastIndexOf('.');
                if (substring == ".unity")
                {
                    int assetsIndex = scene.IndexOf("Assets");
                    Debug.Log(scene.Substring(assetsIndex));

                    if (assetsIndex == -1) continue;

                    menu.AddItem(new GUIContent(extension), false, () =>
                    {
                        EditorSceneManager.OpenScene(scene.Substring(assetsIndex));
                    });
                }
                else
                {
                    menu.AddItem(new GUIContent(extension), true, ()=>{});
                    MakeSceneMenus(scene, new Rect(15, 0, 150, 20));
                }
            }
        }

    }
}
