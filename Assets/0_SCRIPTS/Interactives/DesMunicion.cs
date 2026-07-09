using UnityEngine;
using Mirror;
using NodeCanvas.BehaviourTrees;

public class DesMunicion : NetworkBehaviour
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

    void Update()  //OJO CON LA POSICION DEL UPDATE, ESTAR DONDE DEBE
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
        //DESACTIVA EN EL CLIENTE
        GetComponent<BehaviourTreeOwner>().enabled = false;
        GetComponent<InstanciaMunicion>().enabled = false;

        for (int i = 0; i < objetoCliente.Length; i++)
        {
            objetoCliente[i].SetActive(false);
        }
    }
}
