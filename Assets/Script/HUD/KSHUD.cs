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
            InitializeNewWindow("Inscription Requise Pour Kemia Simulator", string.Empty, Enums.EWindowType.LOGIN_MODAL);
        }
        
        /// <summary>
        /// Initialise une nouvelle fenêtre. Laissez content vide pour une fenêtre de login si besoin.
        /// </summary>
        /// <param name="title">Le titre de la fenêtre</param>
        /// <param name="content">Le contenu du corps de la fenêtre.</param>
        /// <param name="windowType">Le type de la fenêtre  (<see cref="Enums.EWindowType"/>)</param>
        /// <param name="setContentForLoginModal">Utiliser la variable content pour une fenêtre type <see cref="Enums.EWindowType.LOGIN_MODAL"/> ?</param>
        public void InitializeNewWindow(string title, string content, Enums.EWindowType windowType, bool setContentForLoginModal = false){
            switch (windowType)
            {
                case Enums.EWindowType.GENERIC_MODAL:
                    InitializeGenericWindow(title, content);
                    break;
                
                case Enums.EWindowType.LOGIN_MODAL:
                    if (setContentForLoginModal)
                        InitializeLoginWindow(title, content);
                    else
                    {
                        InitializeLoginWindow(title);
                    }
                    break;
            }
        }

        private void InitializeLoginWindow(string title){
            var window = Instantiate(_windowLoginPrefab, _canvas.transform);

            var window_component = window.GetComponentInChildren<KSWindowBase>();
            
            window_component.SetTitle(title);
        }
        
        private void InitializeLoginWindow(string title, string content){
            var window = Instantiate(_windowLoginPrefab, _canvas.transform);

            var window_component = window.GetComponentInChildren<KSWindowBase>();
            
            window_component.SetTitle(title);
            window_component.SetContent(content);
        }
        
        private void InitializeGenericWindow(string title, string content){
            var window = Instantiate(_windowPrefab, _canvas.transform);

            var window_component = window.GetComponentInChildren<KSWindowBase>();
            
            window_component.SetTitle(title);
            window_component.SetContent(content);
        }
    }
}