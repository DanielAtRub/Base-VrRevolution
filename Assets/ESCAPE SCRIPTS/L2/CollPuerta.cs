using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CollPuerta : NetworkBehaviour
{
    [SerializeField]
    private GameObject ManagerPuertaConexiones;
    [SerializeField]
    private string ExplosivoOkTag;
    public bool ConexionOk;

    // Start is called before the first frame update
    void Start()
    {
    }

    [ServerCallback]
    //void OnTriggerEnter(Collider other)
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(ExplosivoOkTag))
        {
            ConexionOk = true;
            ManagerPuertaConexiones.GetComponent<ManagerPuerta>().Refresh();
        }
    }

    [ServerCallback]
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ExplosivoOkTag))
        {
            ConexionOk = false;
            ManagerPuertaConexiones.GetComponent<ManagerPuerta>().Refresh();
        }
    }
}
