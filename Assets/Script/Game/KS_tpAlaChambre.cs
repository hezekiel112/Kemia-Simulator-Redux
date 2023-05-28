using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KS_tpAlaChambre : MonoBehaviour
{
    public Transform voiture;
    public float voitureRadius;
    public bool colision;
    public LayerMask collisionLayers;
    public Transform Chambre;
    public Transform Joueur;


    void FixedUpdate()
    {
        colision = Physics.CheckSphere(voiture.position, voitureRadius, collisionLayers);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(voiture.position, voitureRadius);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && colision)
        {
            Joueur.position = Chambre.position;
        }


    }
}