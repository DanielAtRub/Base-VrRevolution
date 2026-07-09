using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class BaldosaTrigger : NetworkBehaviour
{
    [Header("Conexión")]
    [Tooltip("Arrastra aquí el objeto que tiene el script IvnCompServerNET")]
    [SerializeField]
    private ActivarPorBoton controladorServer;

    private bool yaActivado = false;

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        if (yaActivado) return;

        yaActivado = true; 

        if (controladorServer != null)
        {
            controladorServer.CambiarEstadoComponente(true);
        }
        else
        {
            Debug.LogError("¡Falta asignar el controlador en la baldosa!");
        }
    }
}