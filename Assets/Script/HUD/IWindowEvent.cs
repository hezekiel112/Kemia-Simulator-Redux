using KemiaSimulatorCore.Script.Statics;
using TMPro;

namespace KemiaSimulatorCore.Script.HUD{
    public partial interface IWindowEvent{
        public void OnOkButtonClicked();
        public void OnNoButtonClicked();
        public void OnExitButtonClicked();
        
        public void HideWindow();
        public void ShowWindow();
        
        public void SendCallbackBuffer(Enums.EWindowCallback callback);
    }
    
    public partial interface IWindowEvent{
        public string GetUsernameFieldContent();
        public string GetPasswordFieldContent();
    }
}