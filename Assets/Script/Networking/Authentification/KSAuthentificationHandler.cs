using System;
using System.Collections;
using System.Collections.Generic;
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
                /*
                 // ce player à déjà un compte existant — inutile de l'authentifier manuellement.
                if (AuthenticationService.Instance.SessionTokenExists)
                {
                    SignInAnonymously();

                    return;
                }*/
                
                if (username == string.Empty)
                {
                    username = RandomPlayerName.GetRandomPlayerName();
                }

                try
                {
                    await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
                }
                catch (AuthenticationException ex)
                {
                    // Compare error code to AuthenticationErrorCodes
                    // Notify the player with the proper error message
                    Debug.LogException(ex);
                }
                catch (RequestFailedException ex)
                {
                    // Compare error code to CommonErrorCodes
                    // Notify the player with the proper error message
                    Debug.LogException(ex);
                }
            }
        }

        public bool IsServerReady(){
            return UnityServices.State == ServicesInitializationState.Initialized;
        }
        
        public async void SignInAnonymously(){
            if (!IsServerReady() || AuthenticationService.Instance.IsSignedIn)
            {
                this.kslogwarn($"impossible de se connecter. connexion aux services ugs en cours ou player deja connecter ? signed-in : ({AuthenticationService.Instance.IsSignedIn})");
                return;
            }
            
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                
                if (AuthenticationService.Instance.IsSignedIn)
                    this.kslog($"connexion anonyme réussie signed-in ({AuthenticationService.Instance.IsSignedIn})({AuthenticationService.Instance.PlayerId})");
            }
            
            catch (RequestFailedException ex)
            {
                var (error_msg, error_code) = GetExceptionRequest(ex);
                
                KSHUD.Instance.InitializeNewWindow("Erreur de connection.", $"Impossible de se connecté au serveur.\ncode d'erreur: {error_code}\n'{error_msg}'", Enums.EWindowType.GENERIC_MODAL);
            }
        }

        public static (string, int) GetExceptionRequest(RequestFailedException exception){
            int code = exception.ErrorCode;
            string errorMessage = "Erreur inconnu.";
            
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
        }

        private void OnConnectionExpired(){
            this.kslogerror("connection expirée");
        }
        
        private void OnSignedIn(){
            
        }
        
        private void OnSignedOut(){
            
        }
    }
}
