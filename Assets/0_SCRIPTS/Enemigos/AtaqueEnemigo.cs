using UnityEngine;
using Mirror;

public class AtaqueEnemigo : NetworkBehaviour
{
    [SerializeField]
    private int CausaDaño = 1;
    [SerializeField]
    private AudioSource SonidoAtaque;

    // Start is called before the first frame update
    void Start()
    { 
    }

    public void DañoAtaque(GameObject t)
    {
        Atack(t);
    }

    [Server]
    void Atack(GameObject co)
    {
        SonidoAtaque.Play();
        RpcSonido();
        // Quita vida al player
        co.GetComponentInParent<Player>().PlayerHealth -= CausaDaño;
    }

    [ClientRpc]
    void RpcSonido()
    {
        SonidoAtaque.Play();
    }
}
