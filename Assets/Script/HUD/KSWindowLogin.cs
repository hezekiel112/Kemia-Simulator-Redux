using KemiaSimulatorCore.Script.Networking;
using KemiaSimulatorCore.Script.Statics;

namespace KemiaSimulatorCore.Script.HUD{
    /// <summary>
    ///     Ce script implémente une fonctionnalité de login en étant hérité de la fondation <see cref="KSWindowBase"/>
    /// </summary>
    public class KSWindowLogin : KSWindowBase {
        protected override void OnWindowOpen(){
            OnOkButtonCallback += Sign;
        }

        private void OnDisable(){
            OnOkButtonCallback -= Sign;
        }
        
        private void Sign(){
            this.kslog("test");
        }

        private void Register(){
            // ksauth
        }
    }
}