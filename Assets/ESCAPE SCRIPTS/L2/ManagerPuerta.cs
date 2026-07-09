using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.BehaviourTrees;
using Mirror;

public class ManagerPuerta : NetworkBehaviour
{
    [SerializeField]
    private GameObject ManagerL2;

    [Header("ABRE PUERTA")]
    [SerializeField]
    private GameObject PuertaBehaviour;

    [Header("ZONAS EXPLOSIVOS PUERTA")]
    public  bool ExplosivosOk;
    [SerializeField]
    private GameObject Slot1, Slot2, Slot3, Slot4, Slot5, Slot6;
    [SerializeField]
    private GameObject LefOff, LedTodoOk;

    [SerializeField]
    private GameObject LEDOff, LEDOk;

    //[Header("CAJA DETONACIÓN")]
    //[SerializeField]
    //private GameObject CajaConexionesBehaviour;

    void LateUpdate()
    {
        if (LedTodoOk.activeSelf)
        {
            LEDOff.SetActive(false);
            LEDOk.SetActive(true);
        }
        else
        {
            LEDOff.SetActive(true);
            LEDOk.SetActive(false);
        }
    }
    //SI EL DETONADOR ES ACTIVADO CON LA PALANCA
    public void ActivaPuerta()
    {
        if (ManagerL2.GetComponent<Manager_L2>().Detonador_Conectado_OK &&
            ManagerL2.GetComponent<Manager_L2>().Explosivos_Colocados_OK)
        {
            //EXPLOSIONES DE LOS EXPLOSIVOS Y ABRE LA PUERTA
            PuertaBehaviour.GetComponent<BehaviourTreeOwner>().enabled = true;
        }
    }

    //COLOCACIÓN DE EXPLOSIVOS
    [Server]
    public void Refresh()
    {
        if (Slot1.GetComponent<CollPuerta>().ConexionOk &&
            Slot2.GetComponent<CollPuerta>().ConexionOk &&
            Slot3.GetComponent<CollPuerta>().ConexionOk &&
            Slot4.GetComponent<CollPuerta>().ConexionOk &&
            Slot5.GetComponent<CollPuerta>().ConexionOk &&
            Slot6.GetComponent<CollPuerta>().ConexionOk)
        {
            ExplosivosOk = true;
            LefOff.SetActive(false);
            LedTodoOk.SetActive(true);
            //CajaConexionesBehaviour.GetComponent<BehaviourTreeOwner>().enabled = true; //ELEVA LA CAJA DE CONEXIONES
            //ACTUALIZA MANAGER L2
            ManagerL2.GetComponent<Manager_L2>().Explosivos_Colocados_OK = true;
        }
        else
        {
            ExplosivosOk = false;
            LefOff.SetActive(true);
            LedTodoOk.SetActive(false);
            //ACTUALIZA MANAGER L2
            ManagerL2.GetComponent<Manager_L2>().Explosivos_Colocados_OK = false;
        }
        RpcRefresh(ExplosivosOk);
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
