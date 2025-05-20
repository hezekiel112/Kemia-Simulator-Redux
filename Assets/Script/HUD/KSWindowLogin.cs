using KemiaSimulatorCore.Script.Networking;

namespace KemiaSimulatorCore.Script.HUD{
    /// <summary>
    ///     Ce script implémente une fonctionnalité de login en étant hérité de la fondation <see cref="KSWindowBase"/>
    /// </summary>
    public class KSWindowLogin : KSWindowBase {
        public override async void OnOkButtonClicked(){
            base.OnOkButtonClicked();

            await KSNetworkingBootstrap.Instance.TryConnect();
        }
    }
}