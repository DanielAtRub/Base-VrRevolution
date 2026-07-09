using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.BehaviourTrees;
using Mirror;

public class EscudoEnemigo : NetworkBehaviour
{
    [SerializeField]
    private GameObject Escudo, EscudoCollider;

    // Start is called before the first frame update
    void Start()
    {
    }

    [Server]
    public void ActEscudo()
    {
        EscudoCollider.SetActive(true);
        //Escudo.SetActive(true);
        Escudo.GetComponent<InterfaceAnimManager>().startAppear();
        RpcActEscudo();
    }
    [Server]
    public void DesEscudo()
    {
        EscudoCollider.SetActive(false);
        //Escudo.SetActive(false);
        Escudo.GetComponent<InterfaceAnimManager>().startDisappear();
        RpcDesEscudo();
    }

    [ClientRpc]
    public void RpcActEscudo()
    {
        EscudoCollider.SetActive(true);
        //Escudo.SetActive(true);
        Escudo.GetComponent<InterfaceAnimManager>().startAppear();
    }
    [ClientRpc]
    public void RpcDesEscudo()
    {
        EscudoCollider.SetActive(false);
        //Escudo.SetActive(false);
        Escudo.GetComponent<InterfaceAnimManager>().startDisappear();
    }

}
