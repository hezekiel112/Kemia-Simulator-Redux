using KemiaSimulatorCore.Script.Networking;
using KemiaSimulatorCore.Script.Networking.Authentication;
using KemiaSimulatorCore.Script.Statics;
using Unity.Services.Authentication;

namespace KemiaSimulatorCore.Script.HUD{
    /// <summary>
    ///     Ce script implémente une fonctionnalité de login en étant hérité de la fondation <see cref="KSWindowBase"/>
    /// </summary>
    public class KSWindowLogin : KSWindowBase {
        protected override void OnWindowOpen(){
            if (OnOkButtonCallback == null)
                OnOkButtonCallback = Sign;
            
            _okButton.onClick.RemoveAllListeners();
            _okButton.onClick.AddListener(OnOkButtonCallback.Invoke);
        }
        
        private void OnDisable(){
            OnOkButtonCallback -= Sign;
        }
        
        private void Sign(){
            print("sign");
            KSAuthentificationHandler.Instance.
                SignInWithLogin(GetUsernameFieldContent(), GetPasswordFieldContent());
        }

        private void Register(){
            // ksauth
        }
    }
}