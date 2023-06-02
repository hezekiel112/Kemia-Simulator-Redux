using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KS_MusicQueue : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private MusicProfile[] m_profile;
    [SerializeField] private int DelayTime;

    public bool IsDelayed => DelayTime > 0;

    Action<MusicProfile> m_callback;

    [System.Serializable]
    public struct MusicProfile
    {
        public AudioClip[] Musics;
        public int TargetScene;
    }

    private void Start()
    {
        m_callback += PlayMusic;
        Play(m_profile[UnityEngine.Random.Range(0, m_profile.Length)]);
    }

    private void Update()
    {
        for (int i = 0; i < m_profile.Length; i++)
        {
            if (!m_audioSource.isPlaying && !IsDelayed)
            {
                StartCoroutine(PlayDelayed(m_profile[i], DelayTime));
                return;
            }
        }
    }

    public void Play(MusicProfile profile)
    {
        m_callback?.Invoke(profile);
    }

    public IEnumerator PlayDelayed(MusicProfile profile, int delay)
    {
        yield return new WaitForSeconds(delay);

        Play(profile);
    }

    private void PlayMusic(MusicProfile profile)
    {
        m_audioSource.clip = profile.Musics[UnityEngine.Random.Range(0, profile.Musics.Length)];
        m_audioSource.Play();
    }
}