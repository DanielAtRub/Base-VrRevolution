using UnityEngine;
using Mirror;

public class CollEsferaPuerta : NetworkBehaviour
{
    [SerializeField]
    private GameObject ManagerEsfPuerta;
    [SerializeField]
    private string CollOkTag;

    public bool ConexionOk = false;

    [ServerCallback]
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(CollOkTag))
        {
            CambiarColorGema gema = other.GetComponentInParent<CambiarColorGema>();

            if (gema != null && gema.colorCambiado && !ConexionOk)
            {
                ConexionOk = true;
                ManagerEsfPuerta.GetComponent<ManagerEsferasPuerta>().Refresh();
            }
        }
    }

    [ServerCallback]
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(CollOkTag))
        {
            if (ConexionOk)
            {
                ConexionOk = false;
                ManagerEsfPuerta.GetComponent<ManagerEsferasPuerta>().Refresh();
            }
        }
    }
}