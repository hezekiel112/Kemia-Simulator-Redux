using System.Collections.Generic;
using System.Linq;
using KemiaSimulatorCore.Script.HUD;
using Unity.Services.Core;
using UnityEngine;

namespace KemiaSimulatorCore.Script.Statics {
    /// <summary>
    /// Ensemble d'extensions API
    /// </summary>
    public static class KSRuntime{
        /// <summary>
        /// Ce build est marqué comme release ?
        /// </summary>
        /// <returns>True si ce build n'est pas beta</returns>
        public static bool IsRelease(this GameVersion v){
#if UNITY_EDITOR || IS_BETA_BUILD
            return false;
#endif 

            return true;
        }

        /// <summary>
        ///     Fait disparaitre toutes les fenêtres.
        /// <remarks>
        ///     Cette méthode vérifie si la scène possède des fenêtres attitrées d'un flag persistent.
        ///     Le système n'affichera dans ce cas uniquement les fenêtres possédant un flag d'une priorité inférieur.
        /// </remarks>
        /// </summary>
        public static void HideAllWindow(){
            if (KSWindowRegistry.Instance.WindowsMap.Count == 0)
            {
                KSWindowRegistry.Instance.kslog("no window");
                return;
            }

            var persistent_window = GetAllPersistentsWindow();

            if (persistent_window != null)
            {
                foreach (var t in persistent_window)
                {
                    if (t.WindowFlag != Enums.EWindowFlag.NETWORK_ERROR_2 && t.IsWindowOpen)
                        t.HideWindow();
                }
            }

            foreach (var non_persistent_window in KSWindowRegistry.Instance.WindowsMap)
            {
                if (!non_persistent_window.Value.IsPersistent() && non_persistent_window.Value.IsWindowOpen)
                    non_persistent_window.Value.HideWindow();
            }
        }

        public static List<KSWindowBase> GetAllPersistentsWindow(){
            List<KSWindowBase> persistent_windows_list = new List<KSWindowBase>();
            
            foreach (var window in KSWindowRegistry.Instance.WindowsMap)
            {
                if (window.Value.IsPersistent() && !persistent_windows_list.Contains(window.Value))
                {
                    persistent_windows_list.Add(window.Value);
                }
            }

            if (persistent_windows_list.Count > 0)
                return persistent_windows_list;
            
            return null;
        }
        
        public static bool IsPersistent(this KSWindowBase context){
            return context.WindowFlag == Enums.EWindowFlag.NONE_0;
        }
        
        public static bool IsPersistent(this KSWindowBase context, out Enums.EWindowFlag window_flag){
            if (context.WindowFlag != Enums.EWindowFlag.NONE_0)
            {
                window_flag = context.WindowFlag;

                return true;
            }
            
            window_flag = Enums.EWindowFlag.NONE_0;
            return context.WindowFlag != Enums.EWindowFlag.NONE_0;
        }
        
        /// <summary>
        ///     Fait disparaitre toutes les fenêtres.
        /// <remarks>
        ///     Cette méthode vérifie si la scène possède des fenêtres attitrées d'un flag persistent.
        ///     Le système n'affichera dans ce cas uniquement les fenêtres possédant un flag d'une priorité inférieur.
        /// </remarks>
        /// </summary>
        /// <param name="exceptThisWindow">Fait disparaitre toutes les fenêtres excepter celle-ci.</param>
        public static void HideAllWindow(string exceptThisWindow){
            var persistent_window = GetAllPersistentsWindow();

            if (persistent_window != null)
            {
                foreach (var t in persistent_window)
                {
                    if (t.WindowFlag != Enums.EWindowFlag.NETWORK_ERROR_2 && t.IsWindowOpen)
                    {
                        t.HideWindow();
                    }
                }
            }

            foreach (var non_persistent_window in KSWindowRegistry.Instance.WindowsMap)
            {
                if (!non_persistent_window.Value.IsPersistent() && non_persistent_window.Value.IsWindowOpen)
                    non_persistent_window.Value.HideWindow();
            }
        }
        
        /// <summary>
        /// Renvoie une fenêtre depuis la map ayant le même identifiant.
        /// </summary>
        /// <param name="window_id">L'identifiant de la fenêtre recherchée.</param>
        /// <returns>La fenêtre cible recherchée.</returns>
        public static KSWindowBase GetWindowFromRegistry(string window_id) =>
            KSWindowRegistry.Instance.WindowsMap.GetValueOrDefault(window_id);


        /// <summary>
        /// Renvoie la première fenêtre du type trouvé dans la map.
        /// </summary>
        /// <param name="window_type">Le type de fenêtre recherché.</param>
        /// <returns>La fenêtre du type recherché.</returns>
        public static KSWindowBase GetFirstWindowByType(Enums.EWindowType window_type){
            foreach (var window in KSWindowRegistry.Instance.WindowsMap.Values)
            {
                if (window.WindowType == window_type)
                    return window;
            }
            
            return null;
        }
        
        #if IS_BETA_BUILD
        /// <summary>
        /// Retourne une nouvelle instance de <see cref="GameVersion"/>
        /// </summary>
        /// <param name="custom_beta_id">Remplace  la valeur <see cref="GameVersion.BETA_BUILD_IDENTIFIER"/></param>
        /// <returns>Une nouvelle instance de <see cref="GameVersion"/></returns>
        public static GameVersion GetNewGameVersion(int custom_beta_id = 0){
            if (custom_beta_id == 0)
            {
                var v = new GameVersion(Application.version, GameVersion.BETA_BUILD_IDENTIFIER);

                return v;
            }
            
            return new GameVersion(Application.version, custom_beta_id);
        }
        #else
        /// <summary>
        /// Retourne une nouvelle instance de <see cref="GameVersion"/>
        /// </summary>
        /// <returns>Une nouvelle instance de <see cref="GameVersion"/></returns>
        public static GameVersion GetNewGameVersion(){
            return new GameVersion(Application.version);
        }
        #endif
    }
}