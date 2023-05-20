using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// Script pour afficher une aide dans l'Editor
/// </summary>
public class KS_ToolTip : MonoBehaviour
{
    [Label("Message :"), ResizableTextArea] public string Message;
}