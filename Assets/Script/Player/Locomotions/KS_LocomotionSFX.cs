using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KS_LocomotionSFX : MonoBehaviour
{
    [SerializeField] AudioSource m_audioSource;
    [SerializeField] FirstPersonController player;


    public void PlaySound(AudioClip clip)
    {
        if (player.rb.velocity.x > 0 || player.rb.velocity.z > 0)
        {
            m_audioSource.PlayOneShot(clip);
        }
    }
}