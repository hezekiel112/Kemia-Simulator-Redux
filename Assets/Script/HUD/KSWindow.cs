using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KemiaSimulatorCore.Script.HUD
{
    public class KSWindow : MonoBehaviour, IWindowEvent {
        [Header("Window Infos :")] 
        [SerializeField]  private Button _exitButton;

        [SerializeField] private Button _okButton, _noButton;
        [SerializeField] private TextMeshProUGUI _titleText, _contentText;

        [SerializeField] private UnityEvent _onOkButton, _onNoButton, _onExitButton;
        
        private bool _isWindowOpen = false;

        public bool IsWindowOpen
        {
            get => _isWindowOpen;
        }
        
        private void OnDisable(){
            _isWindowOpen = false;
            _exitButton.onClick.RemoveAllListeners();
        }

        public void OnOkButtonClicked(){
            _onOkButton.Invoke();
        }

        public void OnNoButtonClicked(){
            _onNoButton.Invoke();
        }

        public void HideWindow(){
            _isWindowOpen = false;
            gameObject.SetActive(false);
        }

        private void Start(){
            ShowWindow();
        }

        public void ShowWindow(){
            if (!_exitButton || !_okButton || !_noButton || !_titleText || !_contentText)
                throw new NullReferenceException($"{this} : missconfig!");
            
            _exitButton.onClick.AddListener(HideWindow);
            _okButton.onClick.AddListener(OnOkButtonClicked);
            _noButton.onClick.AddListener(OnNoButtonClicked);
            
            gameObject.SetActive(true);
            _isWindowOpen = true;
        }
        
        public void SetTitle(string title) => _titleText.text = title;
        public void SetContent(string content) => _contentText.text = content;
    }

    public interface IWindowEvent{
        public void OnOkButtonClicked();
        public void OnNoButtonClicked();
        
        public void HideWindow();
        public void ShowWindow();
    }
}