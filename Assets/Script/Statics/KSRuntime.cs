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