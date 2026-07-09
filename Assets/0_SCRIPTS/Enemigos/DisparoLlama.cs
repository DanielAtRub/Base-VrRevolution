using UnityEngine;
using Mirror;

public class DisparoLlama : NetworkBehaviour
{
    [Header("Firing")]
    [SerializeField]
    private ParticleSystem llamaPrefab;
    [SerializeField]
    private BoxCollider llamaCollider;
    [SerializeField]
    private AudioSource SonidoLlama;

    void Start()
    {    
    }

    [Server]
    public void Dispara()
    {
        Fire();
    }
    [Server]
    public void NoDispara()
    {
        NoFire();
    }

    // this is called on the server
    [Server]
    void Fire()
    {
        llamaPrefab.Play();
        if (SonidoLlama) SonidoLlama.Play();
        llamaCollider.enabled = true;
        RpcFire();
    }
    // this is ejecutado on the Enemy that fired for all observers
    [ClientRpc]
    void RpcFire()
    {
        llamaPrefab.Play();
        if (SonidoLlama) SonidoLlama.Play();
        llamaCollider.enabled = true;
    }

    // this is called on the server
    [Server]
    void NoFire()
    {
        llamaPrefab.Stop();
        llamaCollider.enabled = false;
        RpcNoFire();
    }
    // this is ejecutado on the Enemy that fired for all observers
    [ClientRpc]
    void RpcNoFire()
    {
        llamaPrefab.Stop();
        llamaCollider.enabled = false;
    }
}
