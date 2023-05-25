using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KS_InterfaceOrdi : MonoBehaviour
{
    public Transform ordi;
    public LayerMask collisionLayers;
    public float potionRadius;
    public bool colision;
    public GameObject interfaceUi;

    void FixedUpdate()
    {
        colision = Physics2D.OverlapCircle(ordi.position, potionRadius, collisionLayers);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(ordi.position, potionRadius);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && colision)
        {
            //Lorsque Player serai créé stopé c mouvement ici (a ne surtout pas oublier)
            interfaceUi.SetActive(true);

        }

    }

}
