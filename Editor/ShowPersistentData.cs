using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ShowPersistentData : MonoBehaviour
{
     [MenuItem("Tools/ShowFolder/Open Persistent Data Path")]
    private static void OpenPersistentDataPath()
    {
        string path = Application.persistentDataPath;
        path = path.Replace("/", "\\"); // Ensure compatibility with Windows path format

        System.Diagnostics.Process.Start("explorer.exe", path);
    }
}