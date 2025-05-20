using System;
using System.Collections.Generic;
using UnityEngine;

namespace KemiaSimulatorCore.Script.HUD{
    public class KSWindowRegistry : MonoBehaviour{
        private Dictionary<string, KSWindowBase> _windowsMap = new Dictionary<string, KSWindowBase>();

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
            if (!Instance || Instance)
            {
                Instance = this;
            }
        }

        public void AddWindowToRegistry(string windowName, KSWindowBase instance){
            _windowsMap.TryAdd(windowName, instance);
        }

        public void RemoveWindowFromRegistry(string windowName){
            _windowsMap.Remove(windowName);
        }
    }
}