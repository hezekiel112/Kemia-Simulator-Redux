using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KS_RespawnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            other.GetComponent<FirstPersonController>().SpawnPlayerServerRpc(new(19, 3, 0));
            return;
        }
    }
}