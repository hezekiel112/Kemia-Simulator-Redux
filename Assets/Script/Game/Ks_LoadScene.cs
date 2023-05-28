
using UnityEngine;

public class Ks_LoadScene : MonoBehaviour
{
    public void Load(int scene)
    {
        Ks_LoadingScreenBehavior.Instance.LoadSceneAsync(scene);
    }
}