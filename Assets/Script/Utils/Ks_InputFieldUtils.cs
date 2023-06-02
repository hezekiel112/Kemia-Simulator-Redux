using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Ks_InputFieldUtils : MonoBehaviour
{
    [SerializeField] TMP_InputField Target;
    [SerializeField] int minCharNeeded;
    [SerializeField] Button button;

    public void CheckChar(string arg0)
    {
        if ((char)Target.text.Length < minCharNeeded)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
}