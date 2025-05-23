﻿using KemiaSimulatorCore.Script.Statics;
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
        /// <param name="callback">Le nouveau callback pour cette fenêtre.</param>
        public void SendCallbackBuffer(Enums.EWindowCallback callback);
        
        /// <summary>
        /// Assigne un flag persistent pour cette fenêtre.
        /// Ce flag est utilisé pour des méthodes types '<see cref="ShowWindow"/>' ou '<see cref="HideWindow"/>'.
        /// Le système utilise la valeur du flag pour estimer la priorité d'affichage d'une fenêtre.
        /// Par exemple, le flag <see cref="Enums.EWindowFlag.NETWORK_ERROR_2"/> est plus important que <see cref="Enums.EWindowFlag.LOADING_SCREEN_1"/>.
        /// </summary>
        /// <param name="new_flag">Le nouveau flag pour cette fenêtre.</param>
        public void SetPersistantFlag(Enums.EWindowFlag new_flag);
        
        /// <summary>
        /// Assigne un flag par défaut type <see cref="Enums.EWindowFlag.NONE_0"/>
        /// </summary>
        public void ResetFlag();

        /// <summary>
        /// Active ou désactive le bouton de la fenêtre de type 'OK'.
        /// </summary>
        /// <param name="is_ok_button_active">Est-ce que le bouton 'OK' sera activé ?</param>
        public void SetOkButtonPresence(bool is_ok_button_active);
        
        /// <summary>
        /// Active ou désactive le bouton de la fenêtre de type 'EXIT'.
        /// </summary>
        /// <param name="is_exit_button_active">Est-ce que le bouton 'EXIT' sera activé ?</param>
        public void SetExitButtonPresence(bool is_exit_button_active);
        
        
        /// <summary>
        /// Active ou désactive le bouton de la fenêtre de type 'NO'.
        /// </summary>
        /// <param name="is_no_button_active">Est-ce que le bouton 'NO' sera activé ?</param>
        public void SetNoButtonPresence(bool is_no_button_active);
    }
    
    public partial interface IWindowEvent{
        /// <summary>
        /// Renvoie la valeur du champ de caractère <see cref="TMP_InputField"/>
        /// </summary>
        /// <returns>La valeur du champ de caractère 'username' de cette fenêtre.</returns>
        public string GetUsernameFieldContent();
        
        /// <summary>
        /// Renvoie la valeur du champ de caractère <see cref="TMP_InputField"/>
        /// Cette méthode est la même que : <see cref="GetUsernameFieldContent"/>
        /// </summary>
        /// <returns>La valeur du champ de caractère 'password' de cette fenêtre.</returns>
        public string GetPasswordFieldContent();
    }
}