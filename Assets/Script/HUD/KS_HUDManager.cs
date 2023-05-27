using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KS_HUDManager : MonoBehaviour
{
    public static KS_HUDManager instance;

    public KS_Window CurrentActiveButton;

    private void Awake()
    {
        instance = this;
    }

    public void AssignActiveButton(KS_Window button) { CurrentActiveButton = button; }
}