using UnityEngine;
using Mirror;

public class DesNetObjectToEnable : NetworkBehaviour
{
    [SerializeField]
    private GameObject[] objetoServidor;
    [SerializeField]
    private GameObject[] objetoCliente;

    public override void OnStartServer()
    {
        if (isServer)
            servidor();
    }

    public override void OnStartClient()
    {
        if (isClient)
            cliente();
    }

    void Start()
    {
    }

    [Server]
    void servidor()
    {
        for (int i = 0; i < objetoServidor.Length; i++)
        {
            objetoServidor[i].SetActive(false);
        }
    }

    [Client]
    void cliente()
    {
        for (int i = 0; i < objetoCliente.Length; i++)
        {
            objetoCliente[i].SetActive(false);
        }
    }
}
