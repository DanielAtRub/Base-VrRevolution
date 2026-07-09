using UnityEngine;
using Mirror;
using UnityEngine.XR.Interaction.Toolkit; // Necesario para IXRSelectInteractable
using UnityEngine.XR.Interaction.Toolkit.Interactables; // Necesario para XRGrabInteractable

[RequireComponent(typeof(Rigidbody))]
public class ObjetoFuera : NetworkBehaviour
{
    public bool GemaNivel3 = false;
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;

    private Rigidbody rb;
    private XRGrabInteractable grabInteractable; // Más limpio gracias a los 'using'

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Guardamos la posición y rotación exactas al iniciar
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZonaReset"))
        {
            // Si el servidor detecta la caída, ordena el reseteo directamente a todos
            if (isServer)
            {
                RpcEjecutarReseteo(posicionInicial, rotacionInicial);
            }
            // Si un cliente lo ve caer, le pide al servidor que lo haga
            else
            {
                CmdPedirReseteo();
            }
        }
    }

    // --- COMUNICACIÓN EN RED ---

    // requiresAuthority = false permite que el reseteo funcione aunque un objeto sea empujado 
    // por una física, un NPC, o lo muevas manualmente en el editor.
    [Command(requiresAuthority = false)]
    private void CmdPedirReseteo()
    {
        RpcEjecutarReseteo(posicionInicial, rotacionInicial);
    }

    [ClientRpc]
    private void RpcEjecutarReseteo(Vector3 posDestino, Quaternion rotDestino)
    {
        // 1. SOLTAR LOCALMENTE: Si el jugador en esta pantalla lo tiene agarrado, forzamos que lo suelte
        if (grabInteractable != null && grabInteractable.isSelected && grabInteractable.interactionManager != null)
        {
            // Al importar el namespace arriba, este casteo a (IXRSelectInteractable) ya funciona perfectamente
            grabInteractable.interactionManager.CancelInteractableSelection((IXRSelectInteractable)grabInteractable);
        }

        // 2. EL TRUCO DE LAS FÍSICAS: Dormimos el motor físico un instante para forzar el teletransporte
        if (rb != null)
        {
            rb.isKinematic = true; // Corta en seco la inercia y evita que Unity pelee con la nueva posición

            transform.position = posDestino;
            transform.rotation = rotDestino;

            if (!GemaNivel3)
            {
                rb.isKinematic = false; // Despierta el objeto con su nueva ubicación
            }
        }
        else
        {
            transform.position = posDestino;
            transform.rotation = rotDestino;
        }
    }
}