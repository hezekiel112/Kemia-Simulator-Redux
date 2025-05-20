using System;
using System.Threading.Tasks;
using KemiaSimulatorCore.Script.Statics;
using Unity.Services.Core;
using UnityEngine;

namespace KemiaSimulatorCore.Script.Networking
{
    /// <summary>
    ///     Ce script gère les requêtes serveur d'authentification
    /// </summary>
    public class KSNetworkingBootstrap : MonoBehaviour
    {
        private async void Start(){
            try
            {
                await StartUnityServices();
                this.kslog("unity services multijoueur OK");
            }
            catch(Exception e)
            {
                this.kslogerror(e.Message);
            }
        }
        
        private async Task StartUnityServices(){
            await UnityServices.InitializeAsync();
        }
    }
}