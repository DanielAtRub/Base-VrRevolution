using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CollConector : NetworkBehaviour
{
    [SerializeField] private GameObject ManagerCajaConexiones;
    [SerializeField] private string CableOkTag;
    [SerializeField] private LayerMask CableOkLayer;
    [SerializeField] private GameObject LefOff, LedConect;

    public bool ConexionOk = false;

    // Usamos Stay para esperar pacientemente a que el cable se ancle
    [ServerCallback]
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(CableOkTag))
        {
            // 1. Buscamos el script del cable en el objeto padre
            SnapConector cable = other.transform.parent.GetComponent<SnapConector>();

            // 2. Si el cable existe, ESTÁ PEGADO (InSnap == true) y aún NO hemos activado la conexión...
            if (cable != null && cable.InSnap && !ConexionOk)
            {
                int layerDelPadre = other.transform.parent.gameObject.layer;

                if ((CableOkLayer.value & (1 << layerDelPadre)) > 0)
                {
                    // ¡Conexión válida y anclada!
                    ConexionOk = true;
                    LefOff.SetActive(false);
                    LedConect.SetActive(true);
                    RpcRefresh(1);
                    ManagerCajaConexiones.GetComponent<ManagerCajaConexiones>().Refresh();
                }
            }

            // Si InSnap es falso (porque el jugador lo ha vuelto a agarrar), apagamos
            else if (cable != null && !cable.InSnap && ConexionOk)
            {
                Desconectar();
                LefOff.SetActive(true);
                LedConect.SetActive(false);
            }
        }
    }

    [ServerCallback]
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(CableOkTag))
        {
            // Por si acaso sacamos el cable súper rápido y el Stay no lo pilla
            if (ConexionOk)
            {
                Desconectar();
            }
        }
    }

    // He creado una función separada para no repetir código
    [Server]
    void Desconectar()
    {
        ConexionOk = false;
        LefOff.SetActive(true);
        LedConect.SetActive(false);
        RpcRefresh(2);
        ManagerCajaConexiones.GetComponent<ManagerCajaConexiones>().Refresh();
    }

    [ClientRpc]
    void RpcRefresh(int estado)
    {
        if (estado == 1)
        {
            LefOff.SetActive(false);
            LedConect.SetActive(true);
        }
        else if (estado == 2)
        {
            LefOff.SetActive(true);
            LedConect.SetActive(false);
        }
    }
}