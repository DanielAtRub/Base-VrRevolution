using Mirror;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(NetworkIdentity))]
[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class NetworkGrabBridge : NetworkBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private NetworkIdentity netIdentity;

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        netIdentity = GetComponent<NetworkIdentity>();

        // Nos suscribimos al evento de cuando el objeto es agarrado
        grabInteractable.selectEntered.AddListener(OnGrabbed);
    }

    void OnDestroy()
    {
        // Buena pr�ctica: desuscribirse al destruir el objeto
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Usamos isOwned en lugar de hasAuthority para versiones modernas de Mirror
        if (netIdentity.isOwned) return;

        // Comprobamos si el objeto que nos agarra (la mano) es nuestro jugador local
        NetworkIdentity handIdentity = args.interactorObject.transform.GetComponentInParent<NetworkIdentity>();

        if (handIdentity != null && handIdentity.isLocalPlayer)
        {
            // Pedimos autoridad al servidor
            CmdRequestAuthority();
        }
    }

    // El par�metro requiresAuthority = false es vital. 
    // Permite que un cliente que NO tiene autoridad ejecute este comando para pedirla.
    [Command(requiresAuthority = false)]
    private void CmdRequestAuthority(NetworkConnectionToClient sender = null)
    {
        // Quitamos la autoridad al due�o anterior (si otro jugador lo ten�a)
        netIdentity.RemoveClientAuthority();

        // Le damos la autoridad al cliente que acaba de enviar este comando
        netIdentity.AssignClientAuthority(sender);
    }
}