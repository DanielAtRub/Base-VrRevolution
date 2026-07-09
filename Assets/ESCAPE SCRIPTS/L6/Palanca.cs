using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Palanca : NetworkBehaviour
{

    [SerializeField]
    private GameObject ManagPalancas;

    public bool ConexionOk;

    // Start is called before the first frame update
    void Start()
    {
    }

    [Server]
    public void ActPalanca()
    {
        ConexionOk = true;
        //actualiza ManagerPalancas
        ManagPalancas.GetComponent<ManagerPalancas>().Refresh();
    }
    [Server]
    public void DesPalanca()
    {
        ConexionOk = false;
        //actualiza ManagerPalancas
        ManagPalancas.GetComponent<ManagerPalancas>().Refresh();
    }
}
