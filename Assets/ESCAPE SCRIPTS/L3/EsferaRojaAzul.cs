using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EsferaRojaAzul : NetworkBehaviour
{
    [SyncVar]
    public bool AzulOK;
    
    [SerializeField]
    private GameObject EsfRoja, EsfAzul;

    // Start is called before the first frame update
    void Start()
    {
        if (!AzulOK)
        {
            EsfRoja.SetActive(true);
            EsfAzul.SetActive(false);
        }
        else
        {
            EsfRoja.SetActive(false);
            EsfAzul.SetActive(true);
        }
    }

    [Server]
    public void CambiaColorAzul()
    {
        EsfRoja.SetActive(false);
        EsfAzul.SetActive(true);
        RpcCambiaColorAzul();
    }

    [ClientRpc]
    private void RpcCambiaColorAzul()
    {
        EsfRoja.SetActive(false);
        EsfAzul.SetActive(true);
    }
}
