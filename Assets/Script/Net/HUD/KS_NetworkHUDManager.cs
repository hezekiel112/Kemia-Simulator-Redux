using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class KS_NetworkHUDManager : MonoBehaviour
{
    public TextMeshProUGUI PlayerID;

    public TextMeshProUGUI LobbyCode;

    [SerializeField] private TMP_InputField usernameField;


    public static KS_NetworkHUDManager Instance { get;private set; }

    private void Awake()
    {
        PlayerID = PlayerID.gameObject.GetComponent<TextMeshProUGUI>();
        Instance = this;
        LobbyCode = LobbyCode.gameObject.GetComponent<TextMeshProUGUI>();
    }

    public string ChangeID(string id)
    {
        return PlayerID.text = id;
    }

    [ServerRpc(RequireOwnership = true)]
    public void UpdateUsername(TextMeshProUGUI username)
    {
        username.gameObject.GetComponent<TextMeshProUGUI>().text = usernameField.text;
    }
}