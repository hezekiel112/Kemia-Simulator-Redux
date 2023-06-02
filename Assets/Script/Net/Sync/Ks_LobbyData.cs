using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ks_LobbyData : MonoBehaviour
{
    [System.Serializable]
    public struct LobbyData
    {
        public string LobbyToken, LobbyName, LobbyRegion;
    }

    public LobbyData Data;

    public LobbyData CreateLobbyData(string token, string name, string region)
    {
        return Data = new LobbyData()
        {
            LobbyToken = token,
            LobbyName = name,
            LobbyRegion = region
        };
    }
}
