using TMPro;
using UnityEngine;

public class KS_Window : MonoBehaviour, KS_IWindow
{
    public KS_Window Window;

    public virtual void Show()
    {
        Window.gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        Window.gameObject.SetActive(false);
    }

    public virtual void InitializeWindow() { }
}