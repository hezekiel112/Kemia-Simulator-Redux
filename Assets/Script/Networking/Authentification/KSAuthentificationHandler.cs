using System;
using System.Collections;
using System.Collections.Generic;
using KemiaSimulatorCore.Script.Game.Manager;
using KemiaSimulatorCore.Script.Helper;
using KemiaSimulatorCore.Script.HUD;
using KemiaSimulatorCore.Script.Statics;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace KemiaSimulatorCore.Script.Networking.Authentication
{
    /// <summary>
    ///     Ce script gère les fonctionnalités sign-in et sign-up
    /// </summary>
    public class KSAuthentificationHandler : MonoBehaviour
    {
        public static KSAuthentificationHandler Instance
        {
            get;
            private set;
        }
        
        private void Awake(){
            if (!Instance || Instance)
                Instance = this;
        }

        private void Start(){
            SetupAuthentificationListener();
            SignInAnonymously();
        }
        
        private void SetupAuthentificationListener(){
            AuthenticationService.Instance.Expired += OnConnectionExpired;
            AuthenticationService.Instance.SignedIn += OnSignedIn;
            AuthenticationService.Instance.SignedOut += OnSignedOut;
            AuthenticationService.Instance.SignInFailed += OnSignInFailed;
        }

        private void OnDisable(){
            if (UnityServices.State == ServicesInitializationState.Initialized)
            {
                AuthenticationService.Instance.Expired -= OnConnectionExpired;
                AuthenticationService.Instance.SignedIn -= OnSignedIn;
                AuthenticationService.Instance.SignedOut -= OnSignedOut;
                AuthenticationService.Instance.SignInFailed -= OnSignInFailed;
            }
        }
        
        /// <summary>
        /// Inscription d'un joueur
        /// </summary>
        /// <param name="username">Le pseudo du joueur</param>
        /// <param name="password">Le mot de passe du joueur</param>
        public async void SignInWithLogin(string username, string password){
            if (IsServerReady())
            { 
                
                // ce player à déjà un compte existant — inutile de l'authentifier manuellement.
                // if (AuthenticationService.Instance.SessionTokenExists)
                // {
                //     SignInAnonymously();
                // 
                //     return;
                // }
                
                if (username == string.Empty)
                {
                    username = RandomPlayerName.GetRandomPlayerName();
                }

                try
                {
                    await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
                }
                catch (Exception ex) when (ex is AuthenticationException or RequestFailedException)
                {
                    var request_failed = (RequestFailedException)ex;
                    
                    ShowConnectionErrorWindow();
                }
            }
        }

        public bool IsServerReady(){
            return UnityServices.State == ServicesInitializationState.Initialized;
        }
        
        public async void SignInAnonymously(){
            if (!IsServerReady() || AuthenticationService.Instance.IsSignedIn)
            {
                return;
            }

            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

                if (AuthenticationService.Instance.IsSignedIn)
                    this.kslog(
                        $"connexion anonyme réussie signed-in ({AuthenticationService.Instance.IsSignedIn})({AuthenticationService.Instance.PlayerId})");
            }

            catch (Exception ex) when (ex is AuthenticationException or RequestFailedException)
            {
                AuthenticationException request_failed_exception = ex as AuthenticationException;
                
                // var (error_msg, error_code) = GetExceptionRequest(request_failed_exception);

                ShowConnectionErrorWindow();
            }
        }

        public void ShowConnectionErrorWindow(){
            var network_error_windows = KSRuntime.GetAllPersistentsWindow();
            bool has_system_already_network_error_window = false;
            
            if (network_error_windows != null)
            {
                foreach (var network_error_window in network_error_windows)
                {
                    if (network_error_window.WindowFlag == Enums.EWindowFlag.NETWORK_ERROR_2)
                    {
                        if (network_error_window.IsWindowOpen)
                            has_system_already_network_error_window = true;
                    }
                }
            }
            
            if (has_system_already_network_error_window)
                return;
            
            var error_network_window = KSHUD.Instance.InitializeNewWindow(
                "Erreur Connection",
                $"Impossible d'aboutir la requête souhaitée.",
                Enums.EWindowType.NETWORK_ERROR_MODAL, Enums.EWindowFlag.NETWORK_ERROR_2);
            
            // renew buffers
            if (error_network_window)
            {
                if (error_network_window.CallbacksBuffer.Count == 0)
                {
                    error_network_window.SendCallbackBuffer(Enums.EWindowCallback.OK_BTN_REDIRECT_TO_LOGIN_MODAL);
                    error_network_window.SendCallbackBuffer(Enums.EWindowCallback.EXIT_BTN_REDIRECT_TO_EXIT_GAME);
                }
            
                error_network_window.ShowWindow();
            }
        }
        
        public static (string, int) GetExceptionRequest(AuthenticationException exception){
            int code = exception.ErrorCode;
            string errorMessage = "Erreur Inconnue";

            if (exception.ErrorCode == CommonErrorCodes.Conflict)
            {
                errorMessage = "Conflit serveur.";
            }

            if (exception.ErrorCode == CommonErrorCodes.Forbidden)
            {
                errorMessage = "Tentative d'accès non autorisée.";
            }

            if (exception.ErrorCode == CommonErrorCodes.TransportError)
            {
                errorMessage = "Erreur de transite de paquet serveur.";
            }

            if (exception.ErrorCode == CommonErrorCodes.Timeout)
            {
                errorMessage = "Connection non aboutie.";
            }

            if (exception.ErrorCode == CommonErrorCodes.InvalidRequest)
            {
                errorMessage = "Requête web mal formée.";
            }

            if (exception.ErrorCode == CommonErrorCodes.Unknown)
            {
                errorMessage = "Une erreur inconnue est survenue.";
            }

            if (exception.ErrorCode == CommonErrorCodes.ApiMissing)
            {
                errorMessage = "Une erreur inconnue est survenue";
            }

            if (exception.ErrorCode == CommonErrorCodes.InvalidToken)
            {
                errorMessage = "Une erreur inconnue est survenue.";
            }

            if (exception.ErrorCode == CommonErrorCodes.NotFound)
            {
                errorMessage = "La ressource demandée est indisponible pour le moment.";
            }

            if (exception.ErrorCode == CommonErrorCodes.RequestRejected)
            {
                errorMessage = "Requête interdite.";
            }

            if (exception.ErrorCode == CommonErrorCodes.ServiceUnavailable)
            {
                errorMessage = "Le serveur est momentanément indisponible.";
            }

            if (exception.ErrorCode == CommonErrorCodes.TokenExpired)
            {
                errorMessage = "Session joueur expirée. Veuillez reformuler la requête.";
            }

            if (exception.ErrorCode == CommonErrorCodes.TooManyRequests)
            {
                errorMessage = "Le serveur est momentanément indisponible.";
            }

            if (exception.ErrorCode == CommonErrorCodes.PlayerPolicyAccessDenied)
            {
                errorMessage = "Le serveur est momentanément indisponible.";
            }

            if (exception.ErrorCode == CommonErrorCodes.ProjectPolicyAccessDenied)
            {
                errorMessage = "Le serveur est momentanément indisponible.";
            }
            
            return (errorMessage, code);
        }
        
        // todo: reformuler ces erreurs pour les rendres user friendly.
        public static (string, int) GetExceptionRequest(RequestFailedException exception){
            int code = exception.ErrorCode;
            string errorMessage = "Erreur Inconnue.";
            
            // un bordel monstre, s'agirait de clean tout ça vers des strings constants dans /statics.
            
            if (exception.ErrorCode == AuthenticationErrorCodes.BannedUser)
            {
                errorMessage = "Votre compte a été banni. Veuillez contacter le support.";
            }
            else if (exception.ErrorCode == AuthenticationErrorCodes.EnvironmentMismatch)
            {
                errorMessage = "Erreur d'environnement : mauvais environnement Unity (prod/dev).";
            }
            else if (exception.ErrorCode == AuthenticationErrorCodes.InvalidParameters)
            {
                errorMessage = "Paramètres invalides. Vérifiez les champs saisis.";
            }
            else if (exception.ErrorCode == AuthenticationErrorCodes.AccountAlreadyLinked)
            {
                errorMessage = "Ce compte est déjà lié à un autre utilisateur.";
            }
            else if (exception.ErrorCode == AuthenticationErrorCodes.InvalidProvider)
            {
                errorMessage = "Fournisseur d'identité invalide ou non reconnu.";
            }
            else if (exception.ErrorCode == AuthenticationErrorCodes.MinValue)
            {
                errorMessage = "Erreur inconnue (code trop bas).";
            }
            else if (exception.ErrorCode == AuthenticationErrorCodes.ClientInvalidProfile)
            {
                errorMessage = "Profil utilisateur invalide. Veuillez réessayer.";
            }
            else if (exception.ErrorCode == AuthenticationErrorCodes.AccountLinkLimitExceeded)
            {
                errorMessage = "Trop de comptes liés. Vous avez atteint la limite autorisée.";
            }
            else if (exception.ErrorCode == AuthenticationErrorCodes.ClientInvalidUserState)
            {
                errorMessage = "État utilisateur invalide. Veuillez relancer l'application.";
            }
            else if (exception.ErrorCode == AuthenticationErrorCodes.ClientNoActiveSession)
            {
                errorMessage = "Aucune session active. Vous devez vous reconnecter.";
            }
            else if (exception.ErrorCode == AuthenticationErrorCodes.ClientUnlinkExternalIdNotFound)
            {
                errorMessage = "Impossible de délier : compte externe non trouvé.";
            }

            return (errorMessage, code);
        }
        
        private void OnSignInFailed(RequestFailedException exception){
            string errorMessage = string.Empty;

            this.kslogerror("erreur authentification : " + errorMessage + $" ({exception.ErrorCode})");

            ShowConnectionErrorWindow();
        }

        private void OnConnectionExpired(){
            ShowConnectionErrorWindow();
        }
        
        private void OnSignedIn(){
            print("signed in");
            ShowWelcomeWindow();
        }

        private void ShowWelcomeWindow(){
            var welcome_window = KSHUD.Instance.InitializeNewWindow(
                "Kemia Simulator Redux " + GameManager.Instance.GameVersion.BuildVersion,
                $"Bienvenue, {AuthenticationService.Instance.PlayerInfo.Username}",
                Enums.EWindowType.GENERIC_MODAL, Enums.EWindowFlag.NONE_0);

            welcome_window.SendCallbackBuffer(Enums.EWindowCallback.EXIT_BTN_REDIRECT_TO_LOGIN_MODAL);
            welcome_window.ShowWindow();
        }
        
        private void OnSignedOut(){
            
        }
    }
}
