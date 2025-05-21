using System;
using System.Collections;
using System.Collections.Generic;
using KemiaSimulatorCore.Script.Statics;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;

namespace KemiaSimulatorCore.Script.HUD
{
    /// <summary>
    ///     Ce script gére l'HUD joueur (WindowPopup, Field, ...)
    /// </summary>
    public class KSHUD : MonoBehaviour{
        [Header("HUD Prefabs :")] [SerializeField]
        private GameObject _windowPrefab;

        [SerializeField] private GameObject _windowLoginPrefab;
        [SerializeField] private GameObject _windowErrorPrefab;
        
        [Header("Canvas :")][SerializeField] private GameObject _canvas;
        public static KSHUD Instance
        {
            get;
            private set;
        }
        public GameObject WindowPrefab
        {
            get => _windowPrefab;
        }


        private GameObject _windowTempInstance;
        private Button _windowTempCloseButton;
        private string _windowTempTitle, _windowTempContent;
        
        private void Awake(){
            if (Instance || !Instance)
            {
                Instance = this;
            }
        }

        private void Start(){
            Invoke(nameof(ShowLoginWindow), .4f);
        }

        private void ShowLoginWindow(){
            KSRuntime.GetFirstWindowByType(Enums.EWindowType.LOGIN_MODAL).ShowWindow();
        }
        
        /// <summary>
        /// Initialise une nouvelle fenêtre. Laissez content vide pour une fenêtre de login si besoin.
        /// </summary>
        /// <param name="title">Le titre de la fenêtre</param>
        /// <param name="content">Le contenu du corps de la fenêtre.</param>
        /// <param name="windowType">Le type de la fenêtre  (<see cref="Enums.EWindowType"/>)</param>
        /// <param name="setContentForLoginModal">Utiliser la variable content pour une fenêtre type <see cref="Enums.EWindowType.LOGIN_MODAL"/> ?</param>
        public KSWindowBase InitializeNewWindow(string title, string content, Enums.EWindowType windowType, bool setContentForLoginModal = false){
            KSRuntime.HideAllWindow();

            var pooled_window = KSRuntime.GetFirstWindowByType(windowType);

            if (pooled_window)
            {
                switch (windowType)
                {
                    case Enums.EWindowType.NETWORK_ERROR_MODAL:
                    case Enums.EWindowType.GENERIC_MODAL:
                        pooled_window.SetTitle(title);
                        pooled_window.SetContent(content);
                        
                        return pooled_window;

                    case Enums.EWindowType.LOGIN_MODAL:
                        if (setContentForLoginModal)
                        {
                            pooled_window.SetTitle(title);
                            pooled_window.SetContent(content);

                            return pooled_window;
                        }
                        
                        pooled_window.SetTitle(title);
                        return pooled_window;
                }
            }

            this.kslogerror("aucune window de ce type ne peux être créée. " + windowType);
            return null;
        }

        private KSWindowBase InitializeLoginWindow(string title){
            var window = Instantiate(_windowLoginPrefab, _canvas.transform);

            var window_component = window.GetComponentInChildren<KSWindowBase>();
            
            window_component.SetTitle(title);

            return window_component;
        }
        
        private KSWindowBase InitializeLoginWindow(string title, string content){
            var window = Instantiate(_windowLoginPrefab, _canvas.transform);

            var window_component = window.GetComponentInChildren<KSWindowBase>();
            
            window_component.SetTitle(title);
            window_component.SetContent(content);

            return window_component;
        }
        
        private KSWindowBase InitializeGenericWindow(string title, string content){
            var window = Instantiate(_windowPrefab, _canvas.transform);

            var window_component = window.GetComponentInChildren<KSWindowBase>();
            
            window_component.SetTitle(title);
            window_component.SetContent(content);

            return window_component;
        }
    }
}