using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.BehaviourTrees;
using Mirror;

public class ManagerAntorchas : NetworkBehaviour
{
    [SerializeField]
    private GameObject ManagerL4;

    /*[Header("ABRE PUERTA")]
    [SerializeField]
    private GameObject PuertaBehaviour;*/

    [Header("ZONAS ANTORCHAS")]
    [SerializeField]
    private bool AntorchasEncendidasOk;
    [SerializeField]
    private GameObject Antorcha1, Antorcha2, Antorcha3, Antorcha4;//, Antorcha5, Antorcha6;

    [Header("ASCENSORES")]
    [SerializeField]
    private GameObject BehaviourAsc2;
    [SerializeField]
    private GameObject BehaviourAsc3, BehaviourAsc4;

    // Start is called before the first frame update
    void Start()
    {
    }

    //ENCENDIDO DE ANTORCHAS
    [Server]
    public void Refresh()
    {
        if (Antorcha1.GetComponent<CollAntorcha>().LuzEncendida &&
            Antorcha2.GetComponent<CollAntorcha>().LuzEncendida &&
            Antorcha3.GetComponent<CollAntorcha>().LuzEncendida &&
            Antorcha4.GetComponent<CollAntorcha>().LuzEncendida/* &&
            Antorcha5.GetComponent<CollAntorcha>().LuzEncendida &&
            Antorcha6.GetComponent<CollAntorcha>().LuzEncendida*/)
        {
            AntorchasEncendidasOk = true;
            //ACTUALIZA MANAGER L4
            ManagerL4.GetComponent<Manager_L4>().Antorchas_Encendidas_Ok = true;
        }
        else
        {
            AntorchasEncendidasOk = false;
            //ACTUALIZA MANAGER L4
            ManagerL4.GetComponent<Manager_L4>().Antorchas_Encendidas_Ok = false;
        }
        RpcRefresh(AntorchasEncendidasOk);

        //BAJA ASCENSORES
        if (Antorcha1.GetComponent<CollAntorcha>().LuzEncendida)
            BehaviourAsc2.GetComponent<BehaviourTreeOwner>().RestartBehaviour();
        if (Antorcha2.GetComponent<CollAntorcha>().LuzEncendida)
            BehaviourAsc3.GetComponent<BehaviourTreeOwner>().RestartBehaviour();
        if (Antorcha3.GetComponent<CollAntorcha>().LuzEncendida)
            BehaviourAsc4.GetComponent<BehaviourTreeOwner>().RestartBehaviour();
    }
    [ClientRpc]
    void RpcRefresh(bool c)
    {
        /*if (c)
        {
            LefOff.SetActive(false);
            LedTodoOk.SetActive(true);
        }
        else
        {
            LefOff.SetActive(true);
            LedTodoOk.SetActive(false);
        }*/
    }
}
