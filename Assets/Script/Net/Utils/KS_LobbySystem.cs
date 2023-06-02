using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;

public class KS_LobbySystem : MonoBehaviour
{
    public static KS_LobbySystem Instance { get; private set; }

    public static Lobby JoinedLobby { get; private set; }

    [SerializeField] private TMP_InputField lobbyName;
    [SerializeField] private TMP_InputField lobbyID;
    [SerializeField] private Button joinLobbyBtn, createLobbyBtn;

    public string LobbyCode => JoinedLobby.LobbyCode;

    private void Start()
    {
        joinLobbyBtn.onClick.AddListener(() =>
        {
            JoinWithCode(lobbyID.text);
        });

        createLobbyBtn.onClick.AddListener(() =>
        {
            CreateLobby();  
        });
    }

    public async void CreateLobby()
    {
        try
        {
            JoinedLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName.text, 4);

            Allocation alloc = await AllocateRelay();
            string code = await GetRelayJoinCode(alloc);

            await LobbyService.Instance.UpdateLobbyAsync(JoinedLobby.Id, new()
            {
                Data = new Dictionary<string, DataObject>
                {
                    { "RelayJoinCode", new(DataObject.VisibilityOptions.Public, code) }
                }
            });

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(alloc, "dtls"));

            NetworkManager.Singleton.StartHost();
            KS_NetLoader.LoadNetwork(KS_NetLoader.Scene.Game);

            GameManager.Instance.LobbyData.CreateLobbyData(LobbyCode, JoinedLobby.Name, alloc.Region);          
            KS_NetworkHUDManager.Instance.LobbyCode.text = MakeLobbyName();
        }
        catch (LobbyServiceException e) { throw new LobbyServiceException(e); }
    }

    public async void JoinWithCode(string code)
    {
        try
        {
            JoinedLobby = await LobbyService.Instance.JoinLobbyByCodeAsync(code);

            var alloc = await JoinRelay(JoinedLobby.Data["RelayJoinCode"].Value);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(alloc, "dtls"));

            NetworkManager.Singleton.StartClient();
            KS_NetworkHUDManager.Instance.LobbyCode.text = MakeLobbyName(alloc);
        }
        catch (LobbyServiceException e) { throw new LobbyServiceException(e); }
    }

    private async Task<Allocation> AllocateRelay()
    {
        try
        {
            Allocation alloc = await RelayService.Instance.CreateAllocationAsync(4);

            return alloc;
        }
        catch (RelayServiceException e) { throw new LobbyServiceException(e); }
    }

    private async Task<string> GetRelayJoinCode(Allocation alloc)
    {
        try
        {
            string code = await RelayService.Instance.GetJoinCodeAsync(alloc.AllocationId);

            return code;
        }
        catch (RelayServiceException e) { throw new RelayServiceException(e); }
    }

    private async Task<JoinAllocation> JoinRelay(string code)
    {
       try
       {
          JoinAllocation joinAlloc = await RelayService.Instance.JoinAllocationAsync(code);
          return joinAlloc;
       }
       catch (RelayServiceException e) { throw new RelayServiceException(e); }
    }

    private string MakeLobbyName()
    {
        return $"{GameManager.Instance.LobbyData.Data.LobbyName} # {GameManager.Instance.LobbyData.Data.LobbyRegion} [{GameManager.Instance.LobbyData.Data.LobbyToken}]";
    }

    private string MakeLobbyName(JoinAllocation alloc)
    {
        return $"{JoinedLobby.Name} # {alloc.Region} [{JoinedLobby.LobbyCode}]";
    }
}