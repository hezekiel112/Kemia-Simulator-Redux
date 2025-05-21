using System;
using System.Threading.Tasks;
using KemiaSimulatorCore.Script.Networking.Authentication;
using KemiaSimulatorCore.Script.Statics;
using Unity.Services.Authentication;
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
        
        private async void Awake(){
            if (!Instance || Instance)
                Instance = this;
            
            DontDestroyOnLoad(this);

            try
            {
                await UnityServices.InitializeAsync();
            }
            catch (Exception e)
            {
                this.kslogerror("initialisation des services unity échouer! " + e.Message);
            }
        }

        private async void Start(){
            UnityServices.Initialized += OnServicesInitialized;
            UnityServices.InitializeFailed += OnServicesFailed;
            
            try
            {
                await StartUnityServices();
            }
            catch(RequestFailedException e)
            {
                this.kslogerror("connection impossible au serveur ou erreur de connection internet");
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

            KSAuthentificationHandler.Instance.ShowConnectionErrorWindow();
        }
        
        private async Task StartUnityServices(){
            await UnityServices.InitializeAsync();
        }
    }
}