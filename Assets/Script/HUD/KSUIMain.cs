using System;
using System.Collections.Generic;
using KemiaSimulatorCore.Script.Statics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KemiaSimulatorCore.Script.HUD{
    /// <summary>
    ///     Ce script gère l'apparition de l'HUD en fonction de la scène actuelle.
    /// </summary>
    public class KSUIMain : MonoBehaviour{
        [Header("Settings :")]
        [SerializeField] private Enums.ESceneScope _sceneScope;

        [SerializeField] private KSUILoader _uiLoader;
        
        public static KSUIMain Instance
        {
            get;
            private set;
        }

        private void Awake(){
            if (Instance || !Instance)
            {
                Instance = this;
            }
            
            if (!_uiLoader)
                this.kslogerror("uiloader non assigné!");
        }

        private void Start(){
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            
            Invoke(nameof(LoadTitleScreen), .4f);
        }

        private void OnSceneUnloaded(Scene scene){
            var window = KSRuntime.GetFirstWindowByType(Enums.EWindowType.GENERIC_MODAL);
            
            window.SetTitle("Chargement ...");
            window.SetContent("Chargement en cours.\nVeuillez patienter.");
            window.SetPersistantFlag(Enums.EWindowFlag.LOADING_SCREEN);
            
            window.ShowWindow();
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
            var window = KSRuntime.GetFirstWindowByType(Enums.EWindowType.GENERIC_MODAL);
            
            window.SetTitle("Chargement ...");
            window.SetContent("Chargement en cours.\nVeuillez patienter.");
            window.SetPersistantFlag(Enums.EWindowFlag.LOADING_SCREEN);
            
            window.ShowWindow();
        }
        
        private void LoadTitleScreen(){
            Initialize(GetSceneScopeByInt((int)_sceneScope));
        }
        
        public Enums.ESceneScope GetSceneScopeByInt(int scene_scope_int){
            if (Enum.IsDefined(typeof(Enums.ESceneScope), scene_scope_int))
            {
                return (Enums.ESceneScope)scene_scope_int;
            }
            
            this.kslogerror("type int " + scene_scope_int + " invalide pour ESceneScope");

            return Enums.ESceneScope.TITLE_SCREEN;
        }

        public void Initialize(Enums.ESceneScope scene_scope){
            _uiLoader.Load((int)scene_scope);
        }
    }
}