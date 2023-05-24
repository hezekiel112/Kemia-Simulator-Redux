using System.IO;
using UnityEngine;
using UnityEditor;
using NaughtyAttributes;
using NaughtyAttributes.Editor;
using System.Collections;

/// <summary>
/// EditorWindow pour afficher, spawn des objets
/// </summary>

public class Ks_KemiaTools : EditorWindow
{
    private const string PATH = "Assets/Script/Editor/ks_update.txt";

    private string logMessage;

    [MenuItem("Kemia Simulator/Tools")]
    static void Main()
    {
        EditorWindow.CreateInstance<Ks_KemiaTools>().Show();
    }

    private string ReadFile()
    {
        return File.ReadAllText(PATH);
    }

    private void UpdateDebug(string message)
    {
        message = (message + "\n");

        if (logMessage != null) 
        {
            if (logMessage.Contains(message))
            {
                if (!logMessage.Contains($"<color=red>{message}</color>"))
                {
                    logMessage += message = $"<color=red>{message}</color>";
                }
                return;
            }
        }

        logMessage += message;
    }

    private void OnGUI()
    {
        GUIStyle labelStyle = new();

        GUILayout.BeginVertical("box");
        {
            labelStyle.richText = true;

            GUILayout.Label("<color=red>À FAIRE</color>", labelStyle);

            GUILayout.Space(5);

            GUILayout.Label($"<color=white>{ReadFile()}</color>", labelStyle);

            GUILayout.Space(5);
            // -------
        }
        GUILayout.EndVertical();

        GUILayout.Space(25);

        if (GUILayout.Button("Spawn Player"))
            return;

        if (GUILayout.Button("Spawn AI"))
            return;

        if (GUILayout.Button("Check Erreur"))
            return;
    }
}