using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace AITools.ScriptMerger
{

    public class ScriptMerger : EditorWindow
    {
        private string folderPath = "";
        private List<string> filePaths = new List<string>();

        [MenuItem("Tools/Tools for AI/ScriptMerger")]
        public static void ShowWindow()
        {
            GetWindow<ScriptMerger>("ScriptMerger");
        }

        private void OnGUI()
        {
            GUILayout.Label("Folder Path", EditorStyles.boldLabel);
            EditorGUILayout.LabelField(folderPath);

            if (GUILayout.Button("Select Folder"))
            {
                string path = EditorUtility.OpenFolderPanel("Select Folder", "", "");
                if (!string.IsNullOrEmpty(path))
                {
                    folderPath = path;
                    filePaths.Clear(); // Clear file paths if a folder is selected
                }
            }

            GUILayout.Label("Selected Files", EditorStyles.boldLabel);
            foreach (string filePath in filePaths)
            {
                EditorGUILayout.LabelField(filePath);
            }

            if (GUILayout.Button("Select File"))
            {
                string path = EditorUtility.OpenFilePanelWithFilters("Select File", "", new string[] { "C# Files", "cs", "All Files", "*" });
                if (!string.IsNullOrEmpty(path))
                {
                    filePaths.Clear();
                    folderPath = string.Empty; // Clear folder path if files are selected
                    filePaths.Add(path);
                }
            }

            if (GUILayout.Button("Compile and Copy to Clipboard"))
            {
                if (!string.IsNullOrEmpty(folderPath))
                {
                    CompileAndCopyToClipboard(folderPath, "*.cs");
                }
                else if (filePaths.Count > 0)
                {
                    CompileAndCopySelectedFilesToClipboard(filePaths);
                }
                else
                {
                    Debug.LogWarning("No folder or files selected.");
                }
            }
        }

        private void CompileAndCopyToClipboard(string directory, string searchPattern)
        {
            string[] extensions = searchPattern.Split(';');
            List<string> files = new List<string>();

            foreach (string extension in extensions)
            {
                files.AddRange(Directory.GetFiles(directory, extension, SearchOption.AllDirectories));
            }

            StringBuilder stringBuilder = new StringBuilder();

            foreach (string file in files)
            {
                if (!file.EndsWith(".meta"))
                {
                    var content = File.ReadAllText(file);
                    stringBuilder.AppendLine(content);
                }
            }

            EditorGUIUtility.systemCopyBuffer = stringBuilder.ToString();
            Debug.Log("Compiled files copied to clipboard.");
        }

        private void CompileAndCopySelectedFilesToClipboard(List<string> filePaths)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (string file in filePaths)
            {
                if (!file.EndsWith(".meta"))
                {
                    var content = File.ReadAllText(file);
                    stringBuilder.AppendLine(content);
                }
            }

            EditorGUIUtility.systemCopyBuffer = stringBuilder.ToString();
            Debug.Log("Selected files copied to clipboard.");
        }
    }

}