using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            if (player.IsMoving())
            {
                if (player.characterController.velocity.x > 0 || player.characterController.velocity.z > 0)
                {
                    m_audioSource.PlayOneShot(clip);
                }
                return;
            }
        }
        else
            m_audioSource.PlayOneShot(clip);
    }
}