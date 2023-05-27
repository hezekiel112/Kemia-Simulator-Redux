using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class KS_HUD : KS_Window
{
    public enum Menu { Start, Online, Infos, Options, Credits}
    public Menu ActualMenuButton;

    public UnityAction OnStart, OnOnline, OnInfos, OnOptions, OnCredits;

    [SerializeField] private Button button;

    private void Start()
    {
        OnStart += disp_OnStart;
        OnOnline += disp_OnOnline;
        OnInfos += disp_OnInfos;
        OnOptions += disp_OnOptions;
        OnCredits += disp_OnCredits;

        switch (ActualMenuButton)
        {
            case Menu.Start:
                button.onClick.AddListener(OnStart);
                break;

            case Menu.Online:
                button.onClick.AddListener(OnOnline);
                break;

            case Menu.Infos:
                button.onClick.AddListener(OnInfos);
                break;

            case Menu.Options:
                button.onClick.AddListener(OnOptions);
                break;

            case Menu.Credits:
                button.onClick.AddListener(OnCredits);
                break;
        }
    }

    private void disp_OnStart()
    {
        if (KS_HUDManager.instance.CurrentActiveButton is not null)
            KS_HUDManager.instance.CurrentActiveButton.Hide();

        InitializeWindow();
        Show();

        KS_HUDManager.instance.AssignActiveButton(Window);
    }

    private void disp_OnOnline()
    {
        if (KS_HUDManager.instance.CurrentActiveButton is not null)
            KS_HUDManager.instance.CurrentActiveButton.Hide();

        InitializeWindow();
        Show();

        KS_HUDManager.instance.AssignActiveButton(Window);
    }

    private void disp_OnInfos()
    {
        if (KS_HUDManager.instance.CurrentActiveButton is not null)
            KS_HUDManager.instance.CurrentActiveButton.Hide();

        InitializeWindow();
        Show();

        KS_HUDManager.instance.AssignActiveButton(Window);
    }

    private void disp_OnOptions()
    {
        if (KS_HUDManager.instance.CurrentActiveButton is not null)
            KS_HUDManager.instance.CurrentActiveButton.Hide();

        InitializeWindow();
        Show();

        KS_HUDManager.instance.AssignActiveButton(Window);
    }

    private void disp_OnCredits()
    {
        if (KS_HUDManager.instance.CurrentActiveButton is not null)
            KS_HUDManager.instance.CurrentActiveButton.Hide();

        InitializeWindow();
        Show();

        KS_HUDManager.instance.AssignActiveButton(Window);
    }

    public override void Hide()
    {
        base.Hide();
    }

    public override void Show()
    {
        base.Show();
    }

    public override void InitializeWindow()
    {
        var title = Window.transform.Find("Vertical").Find("Title").GetComponent<TextMeshProUGUI>();
        title.text = transform.name;
    }

    public void WhenStartButton() => OnStart?.Invoke();
    public void WhenOnlineButton() => OnOnline?.Invoke();
    public void WhenInfosButton() => OnInfos?.Invoke();
    public void WhenOptionsButton() => OnOptions?.Invoke();
    public void WhenCredits() => OnCredits?.Invoke();
}