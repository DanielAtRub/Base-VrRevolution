using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EsferaRayo : NetworkBehaviour
{
    [SyncVar]
    public bool EsferaRayoActivado;

    [SerializeField]
    private GameObject EsfRoja, EsfAzul;
    [SerializeField]
    private GameObject ManagerL5;

    // Start is called before the first frame update
    void Start()
    {
        if (EsferaRayoActivado)
        {
            EsfRoja.SetActive(false);
            EsfAzul.SetActive(true);
        }else
        {
            EsfRoja.SetActive(true);
            EsfAzul.SetActive(false);
        }
    }

    [Server]
    public void Refresh()
    {
        if (EsferaRayoActivado)
        {
            EsfRoja.SetActive(false);
            EsfAzul.SetActive(true);
            ManagerL5.GetComponent<Manager_L5>().Esfera_Rayo_Ok = true;
        }
        else
        {
            EsfRoja.SetActive(true);
            EsfAzul.SetActive(false);
        }
        RpcRefresh();
    }

    [ClientRpc]
    public void RpcRefresh()
    {
        if (EsferaRayoActivado)
        {
            EsfRoja.SetActive(false);
            EsfAzul.SetActive(true);
        }
        else
        {
            EsfRoja.SetActive(true);
            EsfAzul.SetActive(false);
        }
    }
}
