using System;
using KemiaSimulatorCore.Script.Statics;
using UnityEngine;

namespace KemiaSimulatorCore.Script.Game.Manager{
    /// <summary>
    /// Cette classe persistante gére les fonctionnalités en runtime de Kemia Simulator.
    /// </summary>
    public class GameManager : MonoBehaviour{
        public GameVersion GameVersion
        {
            get;
            private set;
        }

        public static GameManager Instance
        {
            get;
            private set;
        }
        
        private void Awake(){
            if (!Instance || Instance)
            {
                Instance = this;
            }
            
            GameVersion = KSRuntime.GetNewGameVersion();
            this.kslog($"GameVersion OK ({GameVersion.BuildVersion})");
            DontDestroyOnLoad(this);

            Debug.developerConsoleEnabled = true;
        }

        private void Update(){
            Debug.developerConsoleVisible = true;
        }
    }
}