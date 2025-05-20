using UnityEngine;

namespace KemiaSimulatorCore.Script.Statics {
    /// <summary>
    ///     GameVersion est un struct pour stocker la version du build.
    /// </summary>
    public readonly struct GameVersion
    {
        /// <summary>
        ///     Version du build. Lecture seule.
        /// </summary>
        public string BuildVersion { get; }

        #if IS_BETA_BUILD
        public const int BETA_BUILD_IDENTIFIER = 001;
        #endif
        
        /// <summary>
        /// Applique la valeur <see cref="BuildVersion"/> utilise pour afficher la version du build.
        /// </summary>
        /// <param name="version">La version du build.</param>
        public GameVersion(string version){
            BuildVersion = version;
        }
        
        #if IS_BETA_BUILD
        /// <summary>
        /// Applique la valeur <see cref="BuildVersion"/> utilise pour afficher la version beta du build.
        /// </summary>
        /// <param name="version">La version beta du build.</param>
        /// <param name="beta_version_id">L'identifiant beta du build.</param>
        public GameVersion(string version, int beta_version_id){
            BuildVersion = $"{version}-beta{beta_version_id}.{Application.unityVersion}.{Application.platform}";
        }
        #endif
        
        /// <summary>
        /// Si besoin, utiliser ce constructeur pour appliquer un identifiant de version modifié.
        /// </summary>
        /// <param name="major">MàJ Correctif MAJEUR</param>
        /// <param name="minor">MàJ Correctif MINEUR</param>
        /// <param name="fix">MàJ Correctif</param>
        public GameVersion(char major, char minor, char fix){
            BuildVersion = $"{major}.{minor}.{fix}";
        }
        
        /// <summary>
        /// Si besoin, utiliser ce constructeur pour appliquer un identifiant de version modifié.
        /// </summary>
        /// <param name="version">Valeurs de Versioning Semantic (major.minor.fix) ...</param>
        public GameVersion(params char[] version){
            BuildVersion = string.Empty;

            foreach (char t in version)
            {
                BuildVersion = string.Join('.', t);
            }
        }
    }
}