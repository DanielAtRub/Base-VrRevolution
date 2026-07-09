using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RocasCaen : NetworkBehaviour
{
    [SerializeField]
    private Rigidbody[] Piedras;

    // Start is called before the first frame update
    void Start()
    {
    }

    [ServerCallback]
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < Piedras.Length; i++)
            {
                Piedras[i].isKinematic = false;
            }
        }
    }

}
