using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ManagerCajaConexiones : NetworkBehaviour
{
    [SerializeField]
    private GameObject ManagerL2;
    public bool TodoConectadoOK;
    [SerializeField]
    private GameObject Slot1, Slot2, Slot3, Slot4, Slot5, Slot6;
    [SerializeField]
    private GameObject LefOff, LedTodoOk;

    // Start is called before the first frame update
    void Start()
    { 
    }

    [Server]
    public void Refresh()
    {
        if (Slot1.GetComponent<CollConector>().ConexionOk &&
            Slot2.GetComponent<CollConector>().ConexionOk &&
            Slot3.GetComponent<CollConector>().ConexionOk &&
            Slot4.GetComponent<CollConector>().ConexionOk &&
            Slot5.GetComponent<CollConector>().ConexionOk &&
            Slot6.GetComponent<CollConector>().ConexionOk)
        {
            TodoConectadoOK = true;
            LefOff.SetActive(false);
            LedTodoOk.SetActive(true);
            //ACTUALIZA MANAGER L2
            ManagerL2.GetComponent<Manager_L2>().Detonador_Conectado_OK = true;
        }
        else
        {
            TodoConectadoOK = false;
            LefOff.SetActive(true);
            LedTodoOk.SetActive(false);
            //ACTUALIZA MANAGER L2
            ManagerL2.GetComponent<Manager_L2>().Detonador_Conectado_OK = false;
        }
        RpcRefresh(TodoConectadoOK);
    }

    [ClientRpc]
    void RpcRefresh(bool c)
    {
        if (c)
        {
            LefOff.SetActive(false);
            LedTodoOk.SetActive(true);
        }
        else
        {
            LefOff.SetActive(true);
            LedTodoOk.SetActive(false);
        }
    }
}
