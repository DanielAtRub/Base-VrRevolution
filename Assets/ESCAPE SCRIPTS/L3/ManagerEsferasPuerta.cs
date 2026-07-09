using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.BehaviourTrees;
using Mirror;

public class ManagerEsferasPuerta : NetworkBehaviour
{
    [SerializeField]
    private GameObject ManagerL3;

    [Header("ABRE PUERTA")]
    [SerializeField]
    private GameObject PuertaBehaviour;

    [Header("ZONAS ESFERAS PUERTA")]
    [SerializeField]
    public bool EsferasPuertaOk;
    [SerializeField]
    private GameObject Slot1, Slot2, Slot3, Slot4, Slot5, Slot6;

    /*void Update()
    {
        if (Slot1.GetComponent<CollEsferaPuerta>().ConexionOk &&
    Slot2.GetComponent<CollEsferaPuerta>().ConexionOk &&
    Slot3.GetComponent<CollEsferaPuerta>().ConexionOk &&
    Slot4.GetComponent<CollEsferaPuerta>().ConexionOk &&
    Slot5.GetComponent<CollEsferaPuerta>().ConexionOk &&
    Slot6.GetComponent<CollEsferaPuerta>().ConexionOk)
        {
            EsferasPuertaOk = true;
            //LefOff.SetActive(false);
            //LedTodoOk.SetActive(true);
            //CajaConexionesBehaviour.GetComponent<BehaviourTreeOwner>().enabled = true; //ELEVA LA CAJA DE CONEXIONES

            //ABRE LA PUERTA
            PuertaBehaviour.GetComponent<BehaviourTreeOwner>().enabled = true;
            //ACTUALIZA MANAGER L3
            ManagerL3.GetComponent<Manager_L3>().EsferasAzulesOk();
        }
    }*/

    [Server]
    public void Refresh()
    {
        if (Slot1.GetComponent<CollEsferaPuerta>().ConexionOk &&
            Slot2.GetComponent<CollEsferaPuerta>().ConexionOk &&
            Slot3.GetComponent<CollEsferaPuerta>().ConexionOk &&
            Slot4.GetComponent<CollEsferaPuerta>().ConexionOk &&
            Slot5.GetComponent<CollEsferaPuerta>().ConexionOk &&
            Slot6.GetComponent<CollEsferaPuerta>().ConexionOk)
        {
            EsferasPuertaOk = true;
            //LefOff.SetActive(false);
            //LedTodoOk.SetActive(true);
            //CajaConexionesBehaviour.GetComponent<BehaviourTreeOwner>().enabled = true; //ELEVA LA CAJA DE CONEXIONES

            //ABRE LA PUERTA
            PuertaBehaviour.GetComponent<BehaviourTreeOwner>().enabled = true;
            //ACTUALIZA MANAGER L3
            ManagerL3.GetComponent<Manager_L3>().EsferasAzulesOk();
        }
        RpcRefresh(EsferasPuertaOk);
    }
    [ClientRpc]
    void RpcRefresh(bool c)
    {
        if (c)
        {
            ManagerL3.GetComponent<Manager_L3>().EsferasAzulesOk();
        }
    }
}
