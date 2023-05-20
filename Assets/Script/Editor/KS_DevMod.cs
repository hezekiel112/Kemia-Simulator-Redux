using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// Script vérifiant l'intégrité de DevMod InEditor
/// </summary>
[InitializeOnLoad]
public class KS_DevMod : Editor
{
    static bool hasSent = false;

    static KS_DevMod()
    {
        EditorApplication.update += Update;
    }

    private static void Update()
    {
       if (SceneManager.GetActiveScene().name == "!Ks_Dev")
       {
            if (!hasSent)
            {
                Debug.Log("[KS_DevMod] : " + "Le daemon Ks_DevMod est <color=green>démarré</color>\najout des composants nécessaire ...");

                if (GameObject.Find("DevMod")) { return; }

                GameObject devMod = new("DevMod");
                devMod.AddComponent<Ks_DevModHost>();

                Debug.Log("[KS_DevMod] : " + "Composants <color=green>ajoutés</color>");
            }
            hasSent = true;
       }
    }
}