using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Si besoin de cette API, vous devrez avoir ce script dans le même GameObject de votre script utilisant cela
/// Script helper pour l'API DevMod
/// </summary>
[RequireComponent(typeof(Ks_DevModRunner))]
public class Ks_DevModHost : MonoBehaviour, IDevModHandler
{
    public Action<bool> Dm_OnStart;
    public Action<bool> Dm_OnUpdate;

    private void OnEnable()
    {
        if (Initialize())
        {
            Dm_OnStart?.Invoke(true);
            Dm_OnUpdate?.Invoke(true);
        }
    }

    public bool Initialize()
    {
        if (SceneManager.GetActiveScene().name != "!Ks_Dev")
        {
            return false;
        }

        Dm_OnStart += OnStart;
        Dm_OnUpdate += OnUpdate;
        return true;
    }

    public virtual void OnStart(bool state) { }

    public virtual void OnUpdate(bool state)
    {
        
    }
}