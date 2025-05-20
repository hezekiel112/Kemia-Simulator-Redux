using UnityEngine;
using UnityEngine.SceneManagement;

namespace KemiaSimulatorCore.Script.HUD{
    /// <summary>
    ///     Ce script gére les fonctions Load, HasLoaded et Unload pour l'apparition des HUD
    /// </summary>
    public class KSUILoader : MonoBehaviour, IUILoader{

        public void Load(int scene_id){
            if (!SceneManager.GetSceneAt(scene_id).isLoaded)
            {
                SceneManager.LoadScene(scene_id);
            }
        }

        public void Unload(int scene_id){
            if (SceneManager.GetSceneAt(scene_id).isLoaded)
            {
                SceneManager.UnloadSceneAsync(scene_id);
            }
        }

        public bool HasLoaded(int scene_id){
            return SceneManager.GetSceneAt(scene_id).isLoaded;
        }
    }
}