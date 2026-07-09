using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Telepathy;

public class TrampaMata : MonoBehaviour
{
    [SerializeField]
    private int daño = 1;

    void Start()
    {
        //if (isClientOnly)
          //  desColider();
    }

    /*private void desColider()
    {
        GetComponent<BoxCollider>().enabled = false;
    }*/

    [ServerCallback]
    //void OnCollisionEnter(Collision co)
    void OnTriggerEnter(Collider co)
    {
        // Quita vida al player
        if (co.CompareTag ("Player"))
        {
            co.GetComponentInParent<Player>().PlayerHealth -= daño;
        }
    }
}
