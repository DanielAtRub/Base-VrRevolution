using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ManagerPalancas : NetworkBehaviour
{
    [SerializeField]
    private GameObject ManagerL6;
    [SerializeField]
    private bool TodoConectadoOK;
    [SerializeField]
    private GameObject[] Palanca;

    private int cont;

    // Start is called before the first frame update
    void Start()
    {
    }

    [Server]
    public void Refresh()
    {
        /*for (int i = 0; i < Palanca.Length; i++)
        {
            if (Palanca[i].GetComponent<CollConector>().ConexionOk)
                cont++;
           
        }
        if (cont == Palanca.Length)
        {
            TodoConectadoOK = true;
            //ACTUALIZA MANAGER L6
            ManagerL6.GetComponent<Manager_L2>().Detonador_Conectado_OK = true;
        }
        else
        {
            TodoConectadoOK = false;
            //ACTUALIZA MANAGER L2
            ManagerL6.GetComponent<Manager_L2>().Detonador_Conectado_OK = false;
        }*/

        if (Palanca[0].GetComponent<CollConector>().ConexionOk && // CONECTADA
            Palanca[1].GetComponent<CollConector>().ConexionOk && // CONECTADA
            !Palanca[2].GetComponent<CollConector>().ConexionOk && // DESCONECTADA
            !Palanca[3].GetComponent<CollConector>().ConexionOk) // DESCONECTADA
        {
            TodoConectadoOK = true;
            //ACTUALIZA MANAGER L6
            ManagerL6.GetComponent<Manager_L2>().Detonador_Conectado_OK = true;
        }
        else
        {
            TodoConectadoOK = false;
            //ACTUALIZA MANAGER L2
            ManagerL6.GetComponent<Manager_L2>().Detonador_Conectado_OK = false;
        }

       RpcRefresh(TodoConectadoOK);
    }

    [ClientRpc]
    void RpcRefresh(bool c)
    {
        if (c)
        {

        }
        else
        {

        }
    }
}
