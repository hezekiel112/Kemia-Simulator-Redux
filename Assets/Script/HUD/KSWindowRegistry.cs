using System.Collections.Generic;
using UnityEngine;

namespace KemiaSimulatorCore.Script.HUD{
    public class KSWindowRegistry : MonoBehaviour{
        [SerializeField] private List<KSWindowBase> _windows = new List<KSWindowBase>();
        
        private Dictionary<string, KSWindowBase> _windowsMap;

        public Dictionary<string, KSWindowBase> WindowsMap
        {
            get => _windowsMap;
        }

        public static KSWindowRegistry Instance
        {
            get;
            private set;
        }

        private void Awake(){
            _windowsMap = new Dictionary<string, KSWindowBase>();

            foreach (var window in _windows)
            {
                _windowsMap.Add(window.WindowID, window);
            }
            
            if (!Instance || Instance)
            {
                Instance = this;
            }
        }
    }
}