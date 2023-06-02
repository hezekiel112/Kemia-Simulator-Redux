using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Unity.Netcode;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;
using System.Threading.Tasks;

/// <summary>
/// Contient l'API pour la synchronisation P2P et d'autre fonctionnalité
/// </summary>
public class KS_NetworkAPI : MonoBehaviour
{
    private void Awake()
    {
        InitializeAuth();
    }

    private void Start()
    {
        KS_NetworkHUDManager.Instance.ChangeID(AuthenticationService.Instance.PlayerId);
    }

    public async void InitializeAuth()
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
        {
            InitializationOptions profile = new();
            profile.SetProfile($"{Random.Range(0, 1000).ToString()}");

            await UnityServices.InitializeAsync(profile);
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            Debug.Log("<color=green> Connecté !</color>");
        }
    }
}