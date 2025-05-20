using System;
using KemiaSimulatorCore.Script.Statics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KemiaSimulatorCore.Script.HUD{
    /// <summary>
    ///     Ce script fait hériter les autres fenêtres d'une fondation de méthode requise pour le comportement des fenêtres.
    /// </summary>
    public abstract class KSWindowBase : MonoBehaviour, IWindowEvent{
        [Header("Window Infos :")] [SerializeField]
        private Enums.EWindowType _windowType;
        
        [SerializeField]  private Button _exitButton;

        [SerializeField] private Button _okButton, _noButton;
        [SerializeField] private TextMeshProUGUI _titleText, _contentText;

        [SerializeField] private UnityEvent _onOkButton, _onNoButton, _onExitButton;
        
        private bool _isWindowOpen = false;

        public bool IsWindowOpen
        {
            get => _isWindowOpen;
        }

        public Enums.EWindowType WindowType
        {
            get => _windowType;
        }
        
        private void OnDisable(){
            _isWindowOpen = false;
            _exitButton.onClick.RemoveAllListeners();
        }
        
        public virtual void OnOkButtonClicked(){
            _onOkButton.Invoke();
        }

        public virtual void OnNoButtonClicked(){
            _onNoButton.Invoke();
        }
        
        public void SendCallback(string callback) {}
        
        public virtual void OnExitButtonClicked(){
            _onExitButton.Invoke();
        }
        
        public void HideWindow(){
            _isWindowOpen = false;
            Invoke(nameof(HideWindowDelayed), .4f);
        }

        private void HideWindowDelayed(){
            gameObject.SetActive(false);
        }
        
        private void Start(){
            ShowWindow();
        }

        public void ShowWindow(){
            if (!_exitButton || !_okButton || !_noButton || !_titleText || !_contentText)
                throw new NullReferenceException($"{this} : missconfig!");
            
            _exitButton.onClick.AddListener(OnExitButtonClicked);
            _okButton.onClick.AddListener(OnOkButtonClicked);
            _noButton.onClick.AddListener(OnNoButtonClicked);
            
            gameObject.SetActive(true);
            _isWindowOpen = true;
        }
        
        public void SetTitle(string title) => _titleText.text = title;
        public void SetContent(string content) => _contentText.text = content;
    }
}