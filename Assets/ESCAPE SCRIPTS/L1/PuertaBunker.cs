using UnityEngine;
using Mirror;

public class PuertaBunker : NetworkBehaviour
{
    [SerializeField]
    private bool Abierta;
    [SerializeField]
    private AudioSource Sonido;

    // Start is called before the first frame update
    void Start()
    {
    }

    [Server]
    public void AbrePuerta()
    {
        Abierta = true;
        if (Sonido != null)
            Sonido.Play();
        RpcAbrePuerta();
    }

    [ClientRpc]
    void RpcAbrePuerta()
    {
        if (Sonido != null)
            Sonido.Play();
    }
}
