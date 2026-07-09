using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class InvCompRpcClientes : NetworkBehaviour
{
    [SerializeField]
    private NetworkIdentity Padre;

    public UnityEvent Componente;

    // Start is called before the first frame update
    void Start()
    {        
    }

    [Server]
    public void InvComp()
    {
        // Call event
        if (Componente != null)
        {
            Componente.Invoke();
        }
        RpcClient();
    }

    [ClientRpc]
    void RpcClient()
    {
        // Call event
        if (Componente != null)
        {
            Componente.Invoke();
        }
    }
}
