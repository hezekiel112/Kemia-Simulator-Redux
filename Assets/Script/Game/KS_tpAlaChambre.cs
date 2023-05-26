using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KS_tpAlaChambre : MonoBehaviour
{
    public Transform player;
    public Transform voiture;
    public LayerMask collisionLayers;
    public float voitureRadius;
    public bool colision;
    [SerializeField] Transform Chambre;

    void FixedUpdate()
    {
        colision = Physics2D.OverlapCircle(voiture.position, voitureRadius, collisionLayers);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(voiture.position, voitureRadius);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && colision)
        {
            player.transform.position = Chambre.position;
        }

    }
}