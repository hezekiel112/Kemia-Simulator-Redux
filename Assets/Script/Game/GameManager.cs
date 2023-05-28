using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string VersionID;

    [Foldout("Refs Scene Menu"), SerializeField] private TextMeshProUGUI VersionText;

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
}