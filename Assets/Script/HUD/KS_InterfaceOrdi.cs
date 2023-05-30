using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class KS_InterfaceOrdi : MonoBehaviour
{
    public Transform Ordi;
    public GameObject ordi;
    public float OrdiRadius;
    public bool colision;
    public LayerMask collisionLayers;
    public GameObject UiOrdi;
    public Transform Joueur;
    public Transform Snack;

    
    void FixedUpdate()
    {
        colision = Physics.CheckSphere(Ordi.position, OrdiRadius, collisionLayers);
    }


    private void OnDrawGizmos()
    {
        if (ordi != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Ordi.position, OrdiRadius);
            return;
        }
        else
        {
            Debug.Log("Il manque {0} !");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && colision)
        {
            Cursor.lockState = CursorLockMode.None;
            UiOrdi.SetActive(true);
        }
    }

    public void sortirButton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        UiOrdi.SetActive(false);
    }

    public void retourAuSnack()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Joueur.position = Snack.position;
        UiOrdi.SetActive(false);
    }

}
