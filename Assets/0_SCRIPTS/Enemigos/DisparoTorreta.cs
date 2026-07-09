using UnityEngine;
using Mirror;

public class DisparoTorreta : NetworkBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileMount;
    public GameObject Fogonazo;

    void Start()
    {    
    }

    [ServerCallback]
    void Update()
    {
    }

    [Server]
    public void Dispara()
    {
        Fire();
    }
    
    // this is called on the server
    [Server]
    void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileMount.position, projectileMount.rotation);
        NetworkServer.Spawn(projectile);
        fogonazoServidor();
        RpcOnFire();
    }
    
    void fogonazoServidor()
    {
        if (Fogonazo != null)
        {
            Fogonazo.GetComponent<ParticleSystem>().Play();
            Fogonazo.GetComponent<AudioSource>().Play();
        }
    }

    // this is ejecutado on the Enemy that fired for all observers
    [ClientRpc]
    void RpcOnFire()
    {
        if (Fogonazo != null)
        {
            Fogonazo.GetComponent<ParticleSystem>().Play();
            Fogonazo.GetComponent<AudioSource>().Play();
        }
    }

}
