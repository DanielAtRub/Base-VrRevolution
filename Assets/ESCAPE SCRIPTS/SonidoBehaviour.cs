using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SonidoBehaviour : NetworkBehaviour
{
    [SerializeField]
    private AudioSource Sonido;

    // Start is called before the first frame update
    void Start()
    {
    }

    [Server]
    public void ActSonido()
    {
        Sonido.Play();
        RpcActSonido();
    }

    [ClientRpc]
    void RpcActSonido()
    {
        Sonido.Play();
    }
}
