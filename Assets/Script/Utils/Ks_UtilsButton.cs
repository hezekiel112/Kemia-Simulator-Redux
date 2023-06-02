using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Ks_UtilsButton : MonoBehaviour
{
    [SerializeField] Button Target;

    public UnityEvent EstActive, EstDisabled;

    private void Awake()
    {
        Target = GetComponent<Button>();
    }

    public enum ButtonState { Active, Disabled}
    public ButtonState State;

    public void ChangeColor()
    {
        if (Target.targetGraphic.color != Color.red)
        {
            Target.targetGraphic.color = Color.red;
            
            Switch(ButtonState.Disabled);
        }
        else if (Target.targetGraphic.color != Color.green)
        {
            Target.targetGraphic.color = Color.green;
            Switch(ButtonState.Active);
        }

        InitButtonState();
    }

    public ButtonState Switch(ButtonState state)
    {
        return State = state;
    }

    public void InitButtonState()
    {
        switch (State)
        {
            case ButtonState.Active: EstActive?.Invoke(); break;
            case ButtonState.Disabled: EstDisabled?.Invoke(); break;
        }
    }
}