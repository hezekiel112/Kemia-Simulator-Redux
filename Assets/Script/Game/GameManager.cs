using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public string VersionID;

    public Log LogSystem;

    public KS_NetworkAPI NetworkAPI;

    public Ks_LobbyData LobbyData;

    public Ks_NetSpawnObject ObjectsUtils;

    public static Vector3 SnackCoordinate = new(7, 5, -30f);

    [Foldout("Refs Scene Menu"), SerializeField] private TextMeshProUGUI VersionText;

    private void Awake()
    {
        Instance = this;
        NetworkAPI = GetComponent<KS_NetworkAPI>();
        LobbyData = GetComponent<Ks_LobbyData>();
        ObjectsUtils =  GetComponent<Ks_NetSpawnObject>();
    }

    private void Start()
    {
        InitGame();
        DontDestroyOnLoad(this);
    }

    private void InitGame()
    {
        VersionText.text = GetVersionID();
    }

    public string GetVersionID() { return VersionID; }

    public Material GetRandomMaterial(int range)
    {
        var possibleMat = ObjectsUtils.PossibleMaterial;

        return possibleMat[Random.Range(range, possibleMat.Length)];
    }
}