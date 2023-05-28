using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KS_LocomotionSFX : MonoBehaviour
{
    [SerializeField] AudioSource m_audioSource;
    [SerializeField, Tooltip("Selectionner cela si ce script est uniquement pour le joueur")] bool isForPlayer;
    [SerializeField] FirstPersonController player;


    public void PlaySound(AudioClip clip)
    {
        if (isForPlayer)
        {
            if (player.rb.velocity.x > 0 || player.rb.velocity.z > 0)
            {
                m_audioSource.PlayOneShot(clip);
            }
        }
        else
            m_audioSource.PlayOneShot(clip);

        print("played");
    }
}