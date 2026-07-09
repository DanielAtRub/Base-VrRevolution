using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SonidosBehaviour : NetworkBehaviour
{
    [SerializeField]
    private AudioSource[] Sonido;

    // Start is called before the first frame update
    void Start()
    {
    }

    [Server]
    public void ActSonido()
    {
        for (int i = 0; i < Sonido.Length; i++)
        {
            Sonido[i].Play();
        }
        RpcActSonido();
    }

    [ClientRpc]
    void RpcActSonido()
    {
        for (int i = 0; i < Sonido.Length; i++)
        {
            Sonido[i].Play();
        }
    }
}
