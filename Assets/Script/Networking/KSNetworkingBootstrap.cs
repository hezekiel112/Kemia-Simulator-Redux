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
        public static KSNetworkingBootstrap Instance
        {
            get;
            private set;
        }
        
        private void Awake(){
            if (!Instance || Instance)
                Instance = this;
            
            DontDestroyOnLoad(this);
        }

        private async void Start(){
            try
            {
                await StartUnityServices();
                if (UnityServices.State == ServicesInitializationState.Initialized)
                    this.kslog("unity services multijoueur OK");
                else
                {
                    this.kslog("unity services multijoueur non actif");
                    this.kslog(UnityServices.State.ToString());
                }
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