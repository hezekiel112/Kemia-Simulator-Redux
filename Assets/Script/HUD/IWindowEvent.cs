using KemiaSimulatorCore.Script.Statics;
using TMPro;

namespace KemiaSimulatorCore.Script.HUD{
    public partial interface IWindowEvent{
        /// <summary>
        /// Cette méthode est appelée lorsque le joueur appuie sur le bouton type 'OK' de cette fenêtre.
        /// </summary>
        public void OnOkButtonClicked();
        
        /// <summary>
        /// Cette méthode est appelée lorsque le joueur appuie sur le bouton type 'NO' de cette fenêtre.
        /// </summary>
        public void OnNoButtonClicked();
        
        /// <summary>
        /// Cette méthode est appelée lorsque le joueur appuie sur le bouton type 'EXIT' de cette fenêtre.
        /// </summary>
        public void OnExitButtonClicked();
        
        /// <summary>
        /// Cette méthode fait disparaitre cette fenêtre de la scène.
        /// </summary>
        public void HideWindow();
        
        /// <summary>
        /// Cette méthode fait apparaitre cette fenêtre dans la scène.
        /// </summary>
        public void ShowWindow();
        
        /// <summary>
        /// Envoi un buffer callback en mémoire tampon.
        /// Cette méthode utilise un 'callback' pour assigner un listener suivant le type du callback.
        /// </summary>
        /// <param name="callback"></param>
        public void SendCallbackBuffer(Enums.EWindowCallback callback);
        
        /// <summary>
        /// Assigne un flag persistent pour cette fenêtre.
        /// Ce flag sera utilisé pour des méthodes types '<see cref="ShowWindow"/>' ou '<see cref="HideWindow"/>'
        /// </summary>
        /// <param name="new_flag"></param>
        public void SetPersistantFlag(Enums.EWindowFlag new_flag);
        
        /// <summary>
        /// Assigne un flag par défaut type <see cref="Enums.EWindowFlag.NONE"/>
        /// </summary>
        public void ResetFlag();
    }
    
    public partial interface IWindowEvent{
        /// <summary>
        /// Renvoie la valeur du champ de caractère <see cref="TMP_InputField"/>
        /// </summary>
        /// <returns></returns>
        public string GetUsernameFieldContent();
        
        /// <summary>
        /// Renvoie la valeur du champ de caractère <see cref="TMP_InputField"/>
        /// Cette méthode est la même que : <see cref="GetUsernameFieldContent"/>
        /// </summary>
        /// <returns></returns>
        public string GetPasswordFieldContent();
    }
}