using UnityEngine.SceneManagement;
using Unity.Netcode;
using System;
using UnityEngine;
using TMPro;

public static class KS_NetLoader
{
    public enum Scene { Menu, Game, Loading, Lobby }

    public static Scene TargetScene;


    public static void load(Scene scene)
    {
        KS_NetLoader.TargetScene = scene;

        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoadNetwork(Scene targetScene)
    {
        NetworkManager.Singleton.SceneManager.LoadScene("!Ks_EnzoMap", LoadSceneMode.Single);
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(TargetScene.ToString());
    }
}