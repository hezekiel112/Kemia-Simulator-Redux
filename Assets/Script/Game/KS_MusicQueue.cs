using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KS_MusicQueue : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private MusicProfile[] m_profile;
    [SerializeField] private TextMeshProUGUI musicListener;

    Action<MusicProfile> m_callback;

    [System.Serializable]
    public struct MusicProfile
    {
        public AudioClip Music;
    }

    private void Start()
    {
        m_callback += PlayMusic;
        Play(m_profile[UnityEngine.Random.Range(0, m_profile.Length)]);
    }


    private void Update()
    {
        if (!m_audioSource.isPlaying)
        {
            Play(m_profile[UnityEngine.Random.Range(0, m_profile.Length)]);
            return;
        }
    }

    public void Play(MusicProfile profile)
    {
        m_callback?.Invoke(profile);
    }

    private void PlayMusic(MusicProfile profile)
    {
        m_audioSource.clip = profile.Music;
        m_audioSource.Play();

        if (musicListener is not null)
            musicListener.text = profile.Music.name;
    }
}