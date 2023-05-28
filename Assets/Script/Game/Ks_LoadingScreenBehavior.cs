using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ks_LoadingScreenBehavior : MonoBehaviour
{
    public static Ks_LoadingScreenBehavior Instance;
    [SerializeField] private Canvas _loadingScreen;
    [SerializeField] private Image _progressFillBar;
    [SerializeField] private float _target;
    [SerializeField] private TextMeshProUGUI LoadingText;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public async void LoadSceneAsync(int index)
    {
        _target = 0;
        _progressFillBar.fillAmount = 0;

        var scene = SceneManager.LoadSceneAsync(index);
        scene.allowSceneActivation = false;

        _loadingScreen.gameObject.SetActive(true);

        do
        {
            await Task.Delay(100);

            _target = scene.progress;

            LoadingText.text = $"Chargement de l'environnement ... {scene.progress*100}%"; 
        }
        while (scene.progress < 0.9f);

        await Task.Delay(1000);

        scene.allowSceneActivation = true;
        _loadingScreen.gameObject.SetActive(false);
    }

    private void Update()
    {
        _progressFillBar.fillAmount = Mathf.MoveTowards(_progressFillBar.fillAmount, _target, 3 * Time.deltaTime);
    }
}