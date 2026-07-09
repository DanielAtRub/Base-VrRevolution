using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CollEstatua : NetworkBehaviour
{
    [SerializeField]
    private string CollOkTag;
    [SerializeField]
    private bool TengoEsfera, SacoEsfera;
    //[SerializeField] private GameObject EstatuaBehaviour;

    // Start is called before the first frame update
    void Start()
    {
    }

    [ServerCallback]
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CollOkTag))
        {
            if (!TengoEsfera && !other.GetComponentInParent<EsferaRojaAzul>().AzulOK)
            {
                TengoEsfera = true;
                other.GetComponentInParent<EsferaRojaAzul>().AzulOK = true;
                other.GetComponentInParent<EsferaRojaAzul>().CambiaColorAzul();

                gameObject.SetActive(false);
                RpcDisable();
            }
        }
    }

    [ServerCallback]
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(CollOkTag))
        {
            TengoEsfera = false;
            SacoEsfera = true;
            //INCLINA ESTATUA
            //EstatuaBehaviour.SetActive(true);
        }
    }

    [ClientRpc]
    void RpcDisable()
    {
        gameObject.SetActive(false);
    }
}
