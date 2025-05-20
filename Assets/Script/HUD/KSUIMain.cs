using System;
using System.Collections.Generic;
using KemiaSimulatorCore.Script.Statics;
using UnityEngine;

namespace KemiaSimulatorCore.Script.HUD{
    /// <summary>
    ///     Ce script gère l'apparition de l'HUD en fonction de la scène actuelle.
    /// </summary>
    public class KSUIMain : MonoBehaviour{
        [Header("Settings :")]
        private Enums.ESceneScope _sceneScope;

        private KSUILoader _uiLoader;
        
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
        }

        public void Initialize(Enums.ESceneScope scene_scope) =>
            _uiLoader.Load((int)scene_scope);
    }
}