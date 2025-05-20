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

        public bool IsUGSInitialized
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
            UnityServices.Initialized += OnServicesInitialized;
            UnityServices.InitializeFailed += OnServicesFailed;
            try
            {
                await StartUnityServices();
                
            }
            catch(Exception e)
            {
                this.kslogerror(e.Message);
            }
        }

        private void OnDisable(){
            UnityServices.Initialized -= OnServicesInitialized;
            UnityServices.InitializeFailed -= OnServicesFailed;
        }

        private void OnServicesInitialized(){
            this.kslog("unity services multijoueur OK");
            IsUGSInitialized = true;
        }

        private void OnServicesFailed(Exception ex){
            this.kslogerror("unity services multijoueur non actif");
            this.kslogerror(UnityServices.State.ToString());
            this.kslogerror(ex.Message);
        }
        
        private async Task StartUnityServices(){
            await UnityServices.InitializeAsync();
        }
    }
}