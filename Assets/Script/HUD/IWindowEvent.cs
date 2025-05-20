using TMPro;

namespace KemiaSimulatorCore.Script.HUD{
    public partial interface IWindowEvent{
        public void OnOkButtonClicked();
        public void OnNoButtonClicked();
        public void OnExitButtonClicked();
        
        public void HideWindow();
        public void ShowWindow();
        
        public void SendCallback(string callback);
    }
    
    public partial interface IWindowEvent{
        public string GetUsernameFieldContent();
        public string GetPasswordFieldContent();
    }
}