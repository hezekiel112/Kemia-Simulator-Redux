using System;
using System.Collections.Generic;
using KemiaSimulatorCore.Script.Statics;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace KemiaSimulatorCore.Script.HUD{
    /// <summary>
    ///     Ce script fait hériter les autres fenêtres d'une fondation de méthode requise pour le comportement des fenêtres.
    /// </summary>
    public abstract class KSWindowBase : MonoBehaviour, IWindowEvent{
        [Header("Window Infos :")] [SerializeField]
        private string _windowId;
        
        [SerializeField] private Enums.EWindowType _windowType;
        
        [SerializeField]  private Button _exitButton;

        [SerializeField] protected Button _okButton, _noButton;
        [SerializeField] private TextMeshProUGUI _titleText, _contentText;
        
        [Header("Seulement pour les types window login modal :")]
        [SerializeField, ShowIf("_windowType", Enums.EWindowType.LOGIN_MODAL)] private TMP_InputField _usernameInputField;
        [SerializeField, ShowIf("_windowType", Enums.EWindowType.LOGIN_MODAL)] private TMP_InputField _passwordInputField;

        private bool _isWindowOpen = false;
        [SerializeField] private List<Enums.EWindowCallback> _callbackBuffer = new List<Enums.EWindowCallback>();

        public List<Enums.EWindowCallback> CallbacksBuffer
        {
            get => _callbackBuffer;
        }
        public string WindowID
        {
            get => _windowId;
        }
        
        public bool IsWindowOpen
        {
            get => _isWindowOpen;
        }

        public Enums.EWindowType WindowType
        {
            get => _windowType;
        }

        [Button("Générer ID Unique")]
        private void GenerateNewWindowID(){
            int random_number = Random.Range(100, 999);
            
            string new_window_id = $"kswin_{_windowType}_{random_number}".ToLowerInvariant();
            _windowId = new_window_id;
        }
        
        private void OnDisable(){
            _isWindowOpen = false;
            _exitButton.onClick.RemoveAllListeners();
        }

        public string GetPasswordFieldContent(){
            return _passwordInputField.text;
        }

        public string GetUsernameFieldContent(){
            return _usernameInputField.text;
        }
        
        public void OnOkButtonClicked(){
            OnOkButtonCallback?.Invoke();
        }

        public virtual void OnNoButtonClicked(){
            OnNoButtonCallback?.Invoke();
        }
        
        /// <summary>
        /// Envoyé à la fenêtre une surcharge en mémoire tampon d'un événement lié à un bouton.
        /// </summary>
        /// <param name="callback">Le type du callback</param>
        public void SendCallbackBuffer(Enums.EWindowCallback callback){
            _callbackBuffer.Add(callback);

            for (int i = 0; i < _callbackBuffer.Count; i++)
            {
                while (_callbackBuffer.Count > 0)
                {
                    switch (_callbackBuffer[i])
                    {
                        case Enums.EWindowCallback.EXIT_BTN_REDIRECT_TO_EXIT_GAME:
                        case Enums.EWindowCallback.OK_BTN_REDIRECT_TO_EXIT_GAME:
                            // todo: exit game
                            _callbackBuffer.RemoveAt(i);
                            break;
                
                        case Enums.EWindowCallback.EXIT_BTN_REDIRECT_TO_LOGIN_MODAL:
                        case Enums.EWindowCallback.OK_BTN_REDIRECT_TO_LOGIN_MODAL:
                            var window_login = KSRuntime.GetFirstWindowByType(Enums.EWindowType.LOGIN_MODAL);

                            if (window_login)
                            {
                                if (_okButton)
                                {
                                    _okButton.onClick.RemoveAllListeners();
                                    _okButton.onClick.AddListener(window_login.ShowWindow);
                                    _callbackBuffer.RemoveAt(i);
                                }
                                
                                if (_exitButton)
                                {
                                    _exitButton.onClick.RemoveAllListeners();
                                    _exitButton.onClick.AddListener(window_login.ShowWindow);
                                    // _callbackBuffer.RemoveAt(i);
                                }
                            }
                            
                            this.kslog(_callbackBuffer.Count.ToString());
                            break;
                    }
                }
            }
        }
        
        public virtual void OnExitButtonClicked(){
            OnExitButtonCallback?.Invoke();
        }
        
        public void HideWindow(){
            if (_okButton)
                _okButton.onClick.RemoveAllListeners();

            if (_exitButton)
                _exitButton.onClick.RemoveAllListeners();

            if (_noButton)
                _noButton.onClick.RemoveAllListeners();
            
            _isWindowOpen = false;
            Invoke(nameof(HideWindowDelayed), .4f);
        }

        private void HideWindowDelayed(){
            gameObject.SetActive(false);
        }

        private void Start(){
            KSWindowRegistry.Instance.AddWindowToRegistry(_windowId, this);
            
            if (string.IsNullOrEmpty(WindowID))
            {
                this.kslogwarn("id null pour la fenêtre : " + name);
                this.kslogwarn("création d'un id temporaire ... ");
                
                GenerateNewWindowID();
            }

            ShowWindow();
        }

        protected delegate void WhenOkButtonClicked();
        protected WhenOkButtonClicked OnOkButtonCallback;
        
        protected delegate void WhenNoButtonClicked();
        protected WhenNoButtonClicked OnNoButtonCallback;
        
        protected delegate void WhenExitButtonClicked();
        protected WhenExitButtonClicked OnExitButtonCallback;
        
        protected virtual void OnWindowOpen() {}
        
        public void ShowWindow(){
            KSRuntime.HideAllWindow(exceptThisWindow: _windowId);
            
            gameObject.SetActive(true);
            _isWindowOpen = true;
            
            OnWindowOpen();
        }
        
        public void SetTitle(string title) => _titleText.text = title;
        public void SetContent(string content) => _contentText.text = content;
    }
}