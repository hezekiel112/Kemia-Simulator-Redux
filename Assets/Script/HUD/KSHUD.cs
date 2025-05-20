using System;
using System.Collections;
using System.Collections.Generic;
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

        private void InitializeNewWindow(string title, string content){
            
        }
    }
}