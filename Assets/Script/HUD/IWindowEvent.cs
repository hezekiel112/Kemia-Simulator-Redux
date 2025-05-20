namespace KemiaSimulatorCore.Script.HUD{
    public interface IWindowEvent{
        public void OnOkButtonClicked();
        public void OnNoButtonClicked();
        public void OnExitButtonClicked();
        
        public void HideWindow();
        public void ShowWindow();
        
        public void SendCallback(string callback);
    }
}